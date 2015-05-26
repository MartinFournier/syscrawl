using System;
using syscrawl.Models.Levels;

namespace syscrawl.Services.Levels
{
    public class NodeServices
    {
        //        public static Node CreateNode<T>()  where T : Node
        //        {
        //            var t = new T();
        //            return NodeFactory.CreateNode(type, name, this, neighbourNode);
        //
        //        }
        //
        //        public static Node CreateNode(
        //            NodeType type,
        //            string name)
        //        {
        //            Node node;
        //            switch (type)
        //            {
        //                case NodeType.Connector:
        //                    {
        //                        node = ConnectorNode.Create(name);
        //                        break;
        //                    }
        //                case NodeType.Filesystem:
        //                    {
        //                        node = FilesystemNode.Create(name);
        //                        break;
        //                    }
        //                case NodeType.Firewall:
        //                    {
        //                        node = FirewallNode.Create(name);
        //                        break;
        //                    }
        //                case NodeType.Entrance:
        //                    {
        //                        node = EntranceNode.Create(name);
        //                        break;
        //                    }
        //                default:
        //                    {
        //                        return null;
        //                    }
        //            }
        //
        //        }
    }
}

