using UnityEngine;
using NGenerics.DataStructures.General;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace syscrawl.Levels.Graph
{
    public class LevelGraph : Graph<Node>
    {
        public LevelSettings Settings { get; private set; }

        public Level Level { get; private set; }

        Dictionary<Edge<Node>, LineRenderer> EdgeRenderers { get; set; }

        IEnumerable<Node> Nodes
        { 
            get
            { 
                return Vertices.Select(v => v.Data); 
            } 
        }

        public Dictionary<Node, NodePosition> NodesPositions = 
            new Dictionary<Node, NodePosition>();

        public LevelGraph(Level level, LevelSettings settings)
            : base(false)
        {
            Settings = settings;
            Level = level;
        }

        public void InitializePositions()
        {
            var nodes = Nodes.ToList();
            for (var index = 0; index < nodes.Count(); index++)
            {
                NodesPositions.Add(
                    nodes[index], 
                    new NodePosition(index + 1, 0, 1));
            }
        }

        Vertex<Node> GetRandomVertex()
        {
            var v = Vertices.ToList()[
                        Random.Range(0, Vertices.Count)];
            if (v.Data.GetType() == typeof(EntranceNode))
            { 
                return GetRandomVertex(); 
                // will stackoverflow is there's only entrance nodes.. BAH
            }
            return v;
        }

        public Node GetRandomNode()
        {
            return GetRandomVertex().Data;
        }

        public Node CreateNode(NodeType type, string name, Node neighbourNode)
        {
            Node node;
            switch (type)
            {
                case NodeType.Connector:
                    {
                        node = ConnectorNode.Create(Level, name);
                        break;
                    }
                case NodeType.Filesystem:
                    {
                        node = FilesystemNode.Create(Level, name);
                        break;
                    }
                case NodeType.Firewall: 
                    {
                        node = FirewallNode.Create(Level, name);
                        break;
                    }
                case NodeType.EntranceNode:
                    {
                        node = EntranceNode.Create(Level, name);
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            var vertex = AddVertex(node);
            node.Vertex = vertex;

            if (neighbourNode != null)
            {
                var edge = 
                    new Edge<Node>(vertex, neighbourNode.Vertex, false);
                AddEdge(edge);
            }

            return node;
        }

        bool CreateRandomEdge()
        {
            var vertexFrom = GetRandomVertex();
            var vertexTo = GetRandomVertex();

            if (vertexFrom.Equals(vertexTo))
                return false;
            var existingEdge = GetEdge(vertexFrom, vertexTo);
            if (existingEdge != null)
                return false;
            var edge = new Edge<Node>(vertexFrom, vertexTo, false);
            if (ContainsEdge(edge))
                return false;

            AddEdge(edge);
            return true;
        }

        public void AddExtraEdges()
        {
            var vertices = Vertices.ToList();
            var nbVertices = vertices.Count;
            var attempt = 0;
            var maximumEdges = Settings.NodeExtraEdges + nbVertices;

            while (
                attempt < Settings.NodeExtraEdgesAttempts &&
                Edges.Count < maximumEdges)
            {
                attempt++;
                CreateRandomEdge(); 
                // Let's do nothing for now if it fails.
            }
        }

        public void InitializeLineRenderers()
        {
            if (!Edges.Any())
                return;

            EdgeRenderers = new Dictionary<Edge<Node>, LineRenderer>();

            var material = new Material(Shader.Find("Standard"));
            material.color = Color.cyan;

            var edgeWrapper = new GameObject("Edges");
            edgeWrapper.transform.parent = Level.transform;

            foreach (var edge in Edges)
            {
                var edgeObject = new GameObject("Edge");
                edgeObject.transform.parent = edgeWrapper.transform;

                var renderer = edgeObject.AddComponent<LineRenderer>();
                EdgeRenderers.Add(edge, renderer);

                renderer.SetWidth(0.1f, 0.1f);

                renderer.material = material;

                var node1 = edge.FromVertex.Data;
                var node2 = edge.ToVertex.Data;

                var pos1 = NodesPositions[node1].PhysicalPosition;
                var pos2 = NodesPositions[node2].PhysicalPosition;

                renderer.SetPosition(0, pos1);
                renderer.SetPosition(1, pos2);
            }
        }
    }
}

