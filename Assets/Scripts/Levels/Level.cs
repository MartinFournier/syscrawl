using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NGenerics.DataStructures.General;
using syscrawl.Levels.Nodes;
using syscrawl.Levels.Graph;

namespace syscrawl.Levels
{
    public class Level : MonoBehaviour
    {

        private LevelGraph graph;
        private Dictionary<Edge<Node>, LineRenderer> edgeRenderers = null;

        void SetLineRenderers()
        {
            if (graph == null || !graph.Edges.Any())
                return;
            var material = new Material(Shader.Find("Standard"));
            material.color = Color.cyan;

            foreach (var edge in graph.Edges)
            {
                var edgeObject = new GameObject("Edge");
                edgeObject.transform.parent = gameObject.transform;

                var renderer = edgeObject.AddComponent<LineRenderer>();
                edgeRenderers.Add(edge, renderer);

                renderer.SetWidth(0.1f, 0.1f);

                renderer.material = material;

                var node1 = edge.FromVertex.Data;
                var node2 = edge.ToVertex.Data;

                var pos1 = graph.NodesPositions[node1].PhysicalPosition;
                var pos2 = graph.NodesPositions[node2].PhysicalPosition;

                renderer.SetPosition(0, pos1);
                renderer.SetPosition(1, pos2);
            }
        }

        public void Generate(LevelSettings settings)
        {
            graph = new LevelGraph();
            edgeRenderers = new Dictionary<Edge<Node>, LineRenderer>();

            var test = new GameObject("FS");
            test.transform.parent = transform;
            var fs = test.AddComponent<Filesystem>();
            fs.Generate(settings);
            var vertex = graph.AddVertex(fs);


            InstanciateNodes(settings, vertex);
            AddRedundantEdges(settings);

            graph.InitializePositions();

            var positionCoroutine = PositionNodes(settings);
            var graphCoroutine = 
                ForceDirectGraph(
                    graph, positionCoroutine);

            //StartCoroutine (positionCoroutine);
            StartCoroutine(graphCoroutine);
        }

        void InstanciateNodes(
            LevelSettings settings, Vertex<Node> startVertex)
        {
            var nbOfNodes = 
                Random.Range(
                    settings.minimumNodes, 
                    settings.maximumNodes + 1);
            var previousVertex = startVertex;
            for (var x = 0; x < nbOfNodes; x++)
            {
                var nodeObject = new GameObject("Node " + x);
                nodeObject.transform.parent = transform;
                var node = nodeObject.AddComponent<Node>();

                node.Generate(settings);

                var vertexTo = new Vertex<Node>(node);
                var vertexFrom = previousVertex;

                if (Random.value > 0.2)
                { // new splitoff
                    vertexFrom = graph.GetRandomVertex();
                } 

                graph.AddVertex(vertexTo);
                var edge = new Edge<Node>(vertexFrom, vertexTo, false);
                graph.AddEdge(edge);
                previousVertex = vertexTo;
            }
            ;
        }

        private void AddRedundantEdges(LevelSettings settings)
        {
            var vertices = graph.Vertices.ToList();
            var nbVertices = vertices.Count;
            var attempt = 0;
            var maximumEdges = settings.nodeExtraEdges + nbVertices;


            while (
                attempt < settings.nodeExtraEdgesAttempts &&
                graph.Edges.Count < maximumEdges)
            {

                var vertexFrom = vertices[Random.Range(0, nbVertices)];
                var vertexTo = vertices[Random.Range(0, nbVertices)];
                if (vertexFrom.Equals(vertexTo))
                    continue;
                var existingEdge = graph.GetEdge(vertexFrom, vertexTo);
                if (existingEdge != null)
                    continue;
                var edge = new Edge<Node>(vertexFrom, vertexTo, false);
                if (graph.ContainsEdge(edge))
                    continue;

                graph.AddEdge(edge);
            }
        }

        IEnumerator PositionNodes(LevelSettings settings)
        {
            yield return 0;
            foreach (var key in graph.NodesPositions.Keys)
            {
                key.transform.localPosition = 
                    graph.NodesPositions[key].PhysicalPosition;
                yield return 0;
            }
            SetLineRenderers();
        }

        IEnumerator ForceDirectGraph(LevelGraph graph, IEnumerator coroutine)
        {
            return ForceDirectGraph(graph, graph.NodesPositions, coroutine);
        }

        //from https://gist.github.com/radiatoryang/5682034
        IEnumerator ForceDirectGraph<T>(
            Graph<T> graph, 
            Dictionary<T, NodePosition> graphPositions, 
            IEnumerator positionCoroutine)
        {
            // settings
            float attractToCenter = 15f;
            float repulsion = 10f;
            float spacing = 0.4f;
            float stiffness = 10f;
            float damping = 0.7f;
        
            // initialize velocities and positions
            Dictionary<Vertex<T>, Vector2> velocity = 
                new Dictionary<Vertex<T>, Vector2>();
            Dictionary<Vertex<T>, Vector2> position = 
                new Dictionary<Vertex<T>, Vector2>();
        
            foreach (Vertex<T> vert in graph.Vertices)
            {
                velocity.Add(vert, Vector2.zero);
            
                Vector3 bestGuess = Random.onUnitSphere * spacing * 0.5f;
                if (graphPositions.ContainsKey(vert.Data))
                {
                    bestGuess += graphPositions[vert.Data].LogicalPosition;
                }
                else
                {
                    bestGuess += graphPositions[
                        vert.IncidentEdges[0].
                        GetPartnerVertex(vert).Data].LogicalPosition;
                }
                position.Add(vert, new Vector2(bestGuess.x, bestGuess.z));
            }


            float totalEnergy = 10f; // initial
            while (totalEnergy > 1f)
            {
                totalEnergy = 0f;
                foreach (Vertex<T> thisVert in graph.Vertices)
                {
                    Vector2 netForce = Vector2.zero; // running total of kinetic energy for thisVert
                
                    // Coulomb repulsion
                    foreach (Vertex<T> otherVert in graph.Vertices)
                    {
                        if (otherVert == thisVert)
                            continue;
                        Vector2 direction = position[thisVert] - position[otherVert];
                        netForce += (direction.normalized * repulsion) / (Mathf.Pow(direction.magnitude + 0.1f, 2f) * 0.5f);
                    }
                
                    // Hooke attraction
                    foreach (Edge<T> neighbor in thisVert.EmanatingEdges)
                    {
                        Vector2 direction = position[neighbor.ToVertex] - position[thisVert];
                        float displacement = spacing - direction.magnitude;
                        netForce += direction.normalized * (stiffness * displacement * -0.5f);
                    }
                
                    // attract to center
                    netForce += -position[thisVert].normalized * attractToCenter;
                
                    // apply velocity to position
                    velocity[thisVert] = (velocity[thisVert] + (netForce * Time.deltaTime)) * damping;
                    position[thisVert] += velocity[thisVert] * Time.deltaTime;
                    // update running totals too, in case we want to use them outside of this coroutine
                    graphPositions[thisVert.Data].LogicalPosition = 
                        new Vector3(position[thisVert].x, 0f, position[thisVert].y);
                
                    // add thisVert's energy to the running total of all kinetic energy
                    totalEnergy += velocity[thisVert].sqrMagnitude;
                }
                Debug.Log("TOTAL ENERGY: " + totalEnergy.ToString());
                yield return 0;
            }
            Debug.Log("Done");
            StartCoroutine(positionCoroutine);
        }

    }
}