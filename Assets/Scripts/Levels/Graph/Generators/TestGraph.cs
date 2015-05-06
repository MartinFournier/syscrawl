using UnityEngine;
using syscrawl.Levels.Nodes;
using System.Linq;

namespace syscrawl.Levels.Graph.Generators
{
    public static class TestGraph
    {
       public static LevelGraph Generate(Level level, LevelSettings settings) {
            var graph = new LevelGraph(level, settings);
            var nbOfNodes = settings.GetRandomNumberOfNodes();

            var entranceNode = 
                graph.CreateNode(
                    NodeType.Entrance, 
                    "Entrance 0",
                    null);
            nbOfNodes--;

            var connectionNode = 
                graph.CreateNode(
                    NodeType.Connector,
                    "Entrance Connection",
                    entranceNode);
            nbOfNodes--;

            var previousNode = entranceNode;

            for (var x = 0; x < nbOfNodes; x++)
            {
                var neighbourNode = previousNode;
                if (Random.value > 0.2)
                { // new splitoff yay
                    neighbourNode = graph.GetRandomNode();
                } 

                var nodeType = NodeType.Connector;
                var nodeName = "Connector #";
                var randomValue = Random.value;
                if (randomValue < 0.3)
                {
                    nodeType = NodeType.Firewall;
                    nodeName = "Firewall #";
                }
                else if (randomValue < 0.6)
                {
                    nodeType = NodeType.Filesystem;
                    nodeName = "Filesystem #";
                }

                var node = graph.CreateNode(nodeType, nodeName + x, neighbourNode);
                previousNode = node;
            }
            
            AddExtraEdges(graph, settings);
            
            return graph;
        }
        
        private static void AddExtraEdges(LevelGraph graph, LevelSettings settings)
        {
            var vertices = graph.Vertices.ToList();
            var nbVertices = vertices.Count;
            var attempt = 0;
            var maximumEdges = settings.NodeExtraEdges + nbVertices;

            while (
                attempt < settings.NodeExtraEdgesAttempts &&
                graph.Edges.Count < maximumEdges)
            {
                attempt++;
                graph.CreateRandomEdge(); 
                // Let's do nothing for now if it fails.
            }
        }
    }

}