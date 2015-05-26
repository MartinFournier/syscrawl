using NGenerics.DataStructures.General;
using System.Collections.Generic;
using System.Linq;

namespace syscrawl.Models.Levels
{
    public abstract class Node
    {
        bool isUncovered = false;

        public readonly NodeType type;

        public string Name{ get; set; }

        public Vertex<Node> Vertex { get; set; }

        public IEnumerable<Node> GetConnections()
        { 

            var nodes = 
                Vertex.IncidentEdges.
                Select(
                    x => x.GetPartnerVertex(Vertex));
            return nodes.Select(x => x.Data);
        }

        public IEnumerable<Node> GetConnections(
            params Node[] excludedConnections)
        {
            var nodes = GetConnections();
            nodes = nodes.Where(x => !excludedConnections.Contains(x));
            return  nodes;
        }

        protected Node(NodeType type)
        {
            this.type = type;
        }
    }
}

