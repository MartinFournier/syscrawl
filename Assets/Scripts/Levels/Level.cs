using UnityEngine;
using syscrawl.Levels.Graph;
using syscrawl.Levels.Graph.Generators;
using System.Linq;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;
using syscrawl.Extensions;

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

            var nodePosition = 
                new Positioning(
                    Graph, 
                    settings.NodeAngle, 
                    settings.NodeDistance);
            
            nodePosition.Position();
        }
    }
}