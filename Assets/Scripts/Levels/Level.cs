using UnityEngine;
using System.Collections;
using syscrawl.Levels.Graph;
using syscrawl.Levels.Graph.Generators;

namespace syscrawl.Levels
{
   
    public class Level : MonoBehaviour
    {
        public LevelSettings Settings { get; private set; }

        public LevelGraph Graph;

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
            
//             Graph = TestGraph.Generate(this, settings);
            Graph = SpecificGraph.Generate(this, settings);
           
            Graph.InitializePositions();

            var positionCoroutine = PositionNodes(settings);
            ForceDirectedGraph.DoGraph(gameObject, Graph, positionCoroutine);
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