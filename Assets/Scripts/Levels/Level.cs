using UnityEngine;
using System.Collections;
using syscrawl.Levels.Nodes;
using syscrawl.Levels.Graph;

namespace syscrawl.Levels
{
    public class Level : MonoBehaviour
    {
        public LevelSettings Settings { get; private set; }

        LevelGraph Graph;

        void OnGUI()
        {
            if (Graph != null)
            {
                GUI.TextArea(
                    new Rect(10, 10, 200, 200), 
                    Graph.ToString());
            }
        }

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

            var entranceNode = 
                Graph.CreateNode(
                    NodeType.Entrance, 
                    "Entrance 0",
                    null);
            nbOfNodes--;

            var connectionNode = 
                Graph.CreateNode(
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
                    neighbourNode = Graph.GetRandomNode();
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

                var node = Graph.CreateNode(nodeType, nodeName + x, neighbourNode);
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