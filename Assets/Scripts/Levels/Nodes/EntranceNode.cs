using System;
using UnityEngine;

namespace syscrawl.Levels.Nodes
{
    public class EntranceNode : Node
    {
        public static EntranceNode Create(
            Level level, string nodeName)
        {
            var node = 
                Node.Create<EntranceNode>(
                    level, nodeName, NodeType.Entrance);

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(node.Wrapper.transform);

            var material = Resources.Load<Material>("Materials/Nodes/SphereNode");
            var renderer = sphere.GetComponent<Renderer>();
            renderer.material = material;

            var scale = new Vector3(3, 3, 3);

            sphere.transform.localScale = scale;

            return node;
        }
    }
}

