using UnityEngine;
using NGenerics.DataStructures.General;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace syscrawl.Levels.Graph
{
    public class LevelGraph : Graph<Node>
    {

        IEnumerable<Node> Nodes
        { 
            get
            { 
                return Vertices.Select(v => v.Data); 
            } 
        }

        public Dictionary<Node, NodePosition> NodesPositions = 
            new Dictionary<Node, NodePosition>();

        public LevelGraph()
            : base(false)
        {
            
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

        public Vertex<Node> GetRandomVertex()
        {
            return Vertices.ToList()[
                Random.Range(0, Vertices.Count)];
        }
    }
}

