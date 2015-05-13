using syscrawl.Levels.Nodes;
using syscrawl.Levels.Graph;
using NGenerics.DataStructures.General;

namespace syscrawl.Levels.Nodes
{
    public static class NodeFactory
    {
        public static Node CreateNode(
            NodeType type, 
            string name, 
            NodesGraph graph,
            Node neighbourNode)
        {
            Node node;
            switch (type)
            {
                case NodeType.Connector:
                    {
                        node = ConnectorNode.Create(graph.Level, name);
                        break;
                    }
                case NodeType.Filesystem:
                    {
                        node = FilesystemNode.Create(graph.Level, name);
                        break;
                    }
                case NodeType.Firewall: 
                    {
                        node = FirewallNode.Create(graph.Level, name);
                        break;
                    }
                case NodeType.Entrance:
                    {
                        node = EntranceNode.Create(graph.Level, name);
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            var vertex = graph.AddVertex(node);
            node.Vertex = vertex;

            if (neighbourNode != null)
            {
                var edge = 
                    new Edge<Node>(vertex, neighbourNode.Vertex, false);
                graph.AddEdge(edge);
            }

            return node;
        }
    }
}

