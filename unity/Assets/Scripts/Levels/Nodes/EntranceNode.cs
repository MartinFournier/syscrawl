using System;
using UnityEngine;
using syscrawl.Models.Levels;

namespace syscrawl.Levels.Nodes
{
    //    public class EntranceNodeV : Node
    //    {
    //        public static EntranceNodeV Create(
    //            string nodeName)
    //        {
    //            var node =
    //                Node.Create<EntranceNode>(
    //                    nodeName, NodeType.Entrance);
    //
    //            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //            sphere.transform.SetParent(node.Wrapper.transform);
    //
    //            var material = Resources.Load<Material>("Materials/Nodes/SphereNode");
    //            var renderer = sphere.GetComponent<Renderer>();
    //
    //            renderer.material = material;
    //
    //            var scale = new Vector3(3, 3, 3);
    //
    //            sphere.transform.localScale = scale;
    //
    //            sphere.GetComponent<Collider>().enabled = false;
    //            var collider = node.Wrapper.AddComponent<SphereCollider>();
    //            collider.radius = 2f;
    //
    //            return node;
    //        }
    //    }
}

