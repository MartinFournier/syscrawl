using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NGenerics.DataStructures.General;
using syscrawl.Levels.Nodes;
using syscrawl.Levels.Graph;

namespace syscrawl.Levels
{
    public class Level : MonoBehaviour
    {
        public LevelSettings Settings { get; set; }

        LevelGraph Graph;

        public void Generate(LevelSettings settings)
        {
            Settings = settings;

            Graph = new LevelGraph(this, settings);

            InstanciateNodes();

            Graph.AddExtraEdges();
            Graph.InitializePositions();

            var positionCoroutine = PositionNodes(settings);
            ForceDirectedGraph.DoGraph(gameObject, Graph, positionCoroutine);
        }

        void InstanciateNodes()
        {
            var nbOfNodes = Settings.GetRandomNumberOfNodes();

            var filesystemNode = 
                Graph.CreateNode(
                    NodeType.Filesystem, 
                    "Filesystem 1",
                    null);
            nbOfNodes--;

            var previousNode = filesystemNode;

            for (var x = 0; x < nbOfNodes; x++)
            {
                var neighbourNode = previousNode;
                if (Random.value > 0.2)
                { // new splitoff yay
                    neighbourNode = Graph.GetRandomNode();
                } 

                var node = 
                    Graph.CreateNode(
                        NodeType.Connector, 
                        "Node " + x, 
                        neighbourNode);

                previousNode = node;
            }
            ;
        }

        IEnumerator PositionNodes(LevelSettings settings)
        {
            yield return 0;
            foreach (var key in Graph.NodesPositions.Keys)
            {
                key.transform.localPosition = 
                    Graph.NodesPositions[key].PhysicalPosition;
                yield return 0;
            }
            Graph.InitializeLineRenderers();
        }

    }
}