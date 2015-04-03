using UnityEngine;
using syscrawl.Utils;

namespace syscrawl.Levels.Nodes
{
    public class FirewallNode : Node
    {
        void Update()
        {
            transform.Rotate(0, 30 * Time.deltaTime, 0); 
            //rotates 50 degrees per second around z axis
        
            //transform.Rotate(Vector3.up * (1f * Time.deltaTime));

        }

        public static FirewallNode Create(
            Level level, string nodeName)
        {
            var nodeObject = new GameObject(nodeName);
            nodeObject.transform.parent = level.transform;
            var node = nodeObject.AddComponent<FirewallNode>();

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(nodeObject.transform);

            var material = Resources.Load<Material>("Materials/Nodes/Firewall");
            var renderer = cube.GetComponent<Renderer>();
            renderer.material = material;

            var scale = new Vector3(1, 4, 4);

            cube.transform.localScale = scale;

            node.Wrapper = nodeObject;
            return node;
        }
    }
}

