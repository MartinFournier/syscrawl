using UnityEngine;
using syscrawl.Levels.Graph;
using syscrawl.Levels.Graph.Generators;
using syscrawl.Utils;
using syscrawl.Utils.Lerp;

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

        public void Generate(LevelSettings settings)
        {
            Settings = settings;
            
//             Graph = TestGraph.Generate(this, settings);
            Graph = SpecificGraph.Generate(this, settings);

            Positioning = 
                new Positioning(
                Graph, 
                settings.NodeAngle, 
                settings.NodeDistance);

            Positioning.MovedToNode += 
                new Positioning.MovedToNodeEventHandler(MovedToNode);

            Positioning.Position();
        }

        void Start()
        {
            Positioning.ToggleVisibility();
        }

        Lerp<Vector3> cameraLerp = new VectorLerp();

        void MovedToNode(Vector3 newNodePosition)
        {
            Debug.Log("Booyah." + newNodePosition);

            cameraLerp.Curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
            cameraLerp.Activate(
                UnityEngine.Camera.main.transform.position, 
                newNodePosition);
        }

        void Update()
        {
            
            if (cameraLerp.IsComplete)
                return;
            
            var value = cameraLerp.Evaluate(Time.deltaTime);
            UnityEngine.Camera.main.transform.position = value;
        }
    }
}