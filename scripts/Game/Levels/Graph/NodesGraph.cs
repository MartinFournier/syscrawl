using UnityEngine;
using NGenerics.DataStructures.General;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace syscrawl.Levels.Graph
{
    public class NodesGraph : Graph<Node>
    {
        public NodesGraph(Level level, LevelSettings settings)
            : base(false)
        {
            Settings = settings;
            Level = level;
        }

        public LevelSettings Settings { get; private set; }

        public Level Level { get; private set; }

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
                return Nodes.First(n => n.Type == NodeType.Entrance);
            }
        }

        public Node CreateNode(
            NodeType type, 
            string name, 
            Node neighbourNode = null)
        {
            return NodeFactory.CreateNode(type, name, this, neighbourNode);
        }

        public override string ToString()
        {
            return string.Format(
                "LevelGraph:Nodes = {0}", Nodes.Count());
        }
    }
}

