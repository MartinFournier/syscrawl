using UnityEngine;
using syscrawl.Levels.Graph;
using syscrawl.Levels.Graph.Generators;
using syscrawl.Utils;
using syscrawl.Utils.Lerp;
using syscrawl.Camera;

namespace syscrawl.Levels
{
    public class Level : MonoBehaviour
    {
        public LevelSettings Settings { get; private set; }

        public NodesGraph Graph;
        public Positioning Positioning;

        void OnGUI()
        {
            if (Graph != null)
            {
                GUI.TextArea(
                    new Rect(10, 10, 200, 200), 
                    Graph.ToString());
            }
        }

        public void Generate(
            LevelSettings settings, 
            CameraController cameraController)
        {
            Settings = settings;
            
//             Graph = TestGraph.Generate(this, settings);
            Graph = SpecificGraph.Generate(this, settings);

            Positioning = 
                new Positioning(
                Graph, 
                settings.NodeAngle, 
                settings.NodeDistance);

            cameraController.BindPositioning(Positioning);

            Positioning.Position();
        }

        void Start()
        {
            Positioning.ToggleVisibility();
        }
    }
}