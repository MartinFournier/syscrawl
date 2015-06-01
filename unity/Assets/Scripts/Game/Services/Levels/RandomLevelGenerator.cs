using UnityEngine;
using System.Linq;
using NGenerics.DataStructures.General;
using syscrawl.Game.Models;
using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Services.Levels
{
    public class RandomLevelGenerator : ILevelGenerator
    {
        readonly LevelGeneratorConfiguration levelGeneratorConfigurations;

        public RandomLevelGenerator(
            LevelGeneratorConfiguration levelGeneratorConfigurations)
        {
            this.levelGeneratorConfigurations = levelGeneratorConfigurations;
        }

        public LevelGraph Generate()
        {
            return Generate(levelGeneratorConfigurations);
        }

        #region TODO CLEAN

        static LevelGraph Generate(LevelGeneratorConfiguration settings)
        {
            var graph = new LevelGraph();
            var nbOfNodes = settings.GetRandomNumberOfNodes();

            var entranceNode = 
                graph.CreateNode<EntranceNode>(
                    "Entrance 0",
                    null);
            nbOfNodes--;

//            var connectionNode = 
            graph.CreateNode<ConnectorNode>(
                "Entrance Connection",
                entranceNode);
            nbOfNodes--;

            var previousNode = entranceNode;

            for (var x = 0; x < nbOfNodes; x++)
            {
                var neighbourNode = previousNode;
                if (Random.value > 0.2)
                { // new splitoff yay
                    neighbourNode = GetRandomNode(graph);
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

        static void AddExtraEdges(LevelGraph graph, LevelGeneratorConfiguration settings)
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
                CreateRandomEdge(graph); 
                // Let's do nothing for now if it fails.
            }
        }

        static bool CreateRandomEdge(LevelGraph graph)
        {
            var vertexFrom = GetRandomVertex(graph);
            var vertexTo = GetRandomVertex(graph);

            if (vertexFrom.Equals(vertexTo))
                return false;
            var existingEdge = graph.GetEdge(vertexFrom, vertexTo);
            if (existingEdge != null)
                return false;
            var edge = new Edge<Node>(vertexFrom, vertexTo, false);
            if (graph.ContainsEdge(edge))
                return false;

            graph.AddEdge(edge);
            return true;
        }

        static Vertex<Node> GetRandomVertex(LevelGraph graph)
        {
            var v = graph.Vertices.ToList()[
                        Random.Range(0, graph.Vertices.Count)];
            if (v.Data.GetType() == typeof(EntranceNode))
            { 
                return GetRandomVertex(graph); 
                // will stackoverflow is there's only entrance nodes.. BAH
            }
            return v;
        }

        static Node GetRandomNode(LevelGraph graph)
        {
            return GetRandomVertex(graph).Data;
        }

        #endregion
    }

}