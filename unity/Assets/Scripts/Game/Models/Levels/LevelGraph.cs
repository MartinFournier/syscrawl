using NGenerics.DataStructures.General;
using syscrawl.Models.Levels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace syscrawl.Models.Levels
{
    public class LevelGraph : Graph<Node>
    {
        public LevelGraph()
            : base(false)
        {

        }

        public IEnumerable<Node> Nodes
        { 
            get
            { 
                return Vertices.Select(v => v.Data); 
            } 
        }

        public Node Entrance
        {
            get
            {
                return Nodes.First(n => n.type == NodeType.Entrance);
            }
        }

        public Node CreateNode(
            NodeType NodeType,
            string name,
            Node neighbourNode = null)
        {
            Node node = null;
            // Can't use a Type variable for the generic method unless we use 
            // reflection which would be meh. So we copy the CreateNode methods.
            switch (NodeType)
            {
                case NodeType.Connector:
                    node = CreateNode<ConnectorNode>(name, neighbourNode);
                    break;
                case NodeType.Entrance:
                    node = CreateNode<EntranceNode>(name, neighbourNode);
                    break;
                case NodeType.Filesystem:
                    node = CreateNode<FilesystemNode>(name, neighbourNode);
                    break;
                case NodeType.Firewall:
                    node = CreateNode<FirewallNode>(name, neighbourNode);
                    break;
            }
            return node;
        }

        public Node CreateNode<T>(
            string name, 
            Node neighbourNode = null) where T: Node, new()
        {
            var node = new T();
            var vertex = AddVertex(node);
            node.Name = name;
            node.Vertex = vertex;

            if (neighbourNode == null)
                return node;

            var edge = 
                new Edge<Node>(
                    vertex, 
                    neighbourNode.Vertex, 
                    false
                );
            AddEdge(edge);

            return node;
        }

        public override string ToString()
        {
            return string.Format(
                "LevelGraph:Nodes = {0}", Nodes.Count());
        }
    }
}

