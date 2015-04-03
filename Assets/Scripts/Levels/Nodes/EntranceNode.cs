using System;
using UnityEngine;

namespace syscrawl.Levels.Nodes
{
    public class EntranceNode : Node
    {
        public static EntranceNode Create(
            Level level, string nodeName)
        {
            var nodeObject = new GameObject(nodeName);
            nodeObject.transform.parent = level.transform;
            var node = nodeObject.AddComponent<EntranceNode>();

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(nodeObject.transform);

            var material = Resources.Load<Material>("Materials/Nodes/SphereNode");
            var renderer = sphere.GetComponent<Renderer>();
            renderer.material = material;

            var scale = new Vector3(3, 3, 3);

            sphere.transform.localScale = scale;

            node.Wrapper = nodeObject;
            return node;
        }
    }
}

