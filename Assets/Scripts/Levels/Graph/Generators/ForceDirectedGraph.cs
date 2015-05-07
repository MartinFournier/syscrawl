using System.Collections;
using NGenerics.DataStructures.General;
using System.Collections.Generic;
using UnityEngine;


namespace syscrawl.Levels.Graph.Generators
{
    public class ForceDirectedGraph : MonoBehaviour
    {
        const int maxIters = 50;

        public static void DoGraph(GameObject parent, LevelGraph graph, IEnumerator coroutine)
        {
            var forceDirectedGraph = parent.AddComponent<ForceDirectedGraph>();

            var graphCoroutine =
                forceDirectedGraph.ForceDirectGraph(
                    graph, coroutine);

            forceDirectedGraph.StartCoroutine(graphCoroutine);
        }

        public IEnumerator ForceDirectGraph(LevelGraph graph, IEnumerator coroutine)
        {
            return ForceDirectGraph(graph, graph.NodesPositions, coroutine);
        }

        //from https://gist.github.com/radiatoryang/5682034
        IEnumerator ForceDirectGraph<T>(
            Graph<T> graph,
            Dictionary<T, NodePosition> graphPositions,
            IEnumerator positionCoroutine)
        {
            Debug.Log("Graph start");
            // settings
            float attractToCenter = 5;
            float repulsion = 25f;
            float spacing = 1f;
            float stiffness = 0.1f;
            float damping = 0.8f;

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

            var iters = 0;
            float totalEnergy = 10f; // initial
            while (totalEnergy > 1f)
            {
                totalEnergy = 0f;
                foreach (Vertex<T> thisVert in graph.Vertices)
                {
                    Vector2 netForce = Vector2.zero;
                    // running total of kinetic energy for thisVert

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
                iters++;
                Debug.Log("TOTAL ENERGY: " + totalEnergy.ToString() + " iteration #" + iters);

                if (iters > maxIters)
                    totalEnergy = 0;
                yield return 0;
            }
            StartCoroutine(positionCoroutine);
        }
    }
}

