using System;
using UnityEngine;
using syscrawl.Utils;
using System.Linq;
using syscrawl.Models.Levels;

namespace syscrawl.Levels.Nodes
{
    //    public class ConnectorNodeV : Node
    //    {
    //        public static ConnectorNodeV Create(
    //            string nodeName)
    //        {
    //            var node =
    //                Node.Create<ConnectorNode>(
    //                    nodeName, NodeType.Connector);
    //
    //            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //            cube.transform.SetParent(node.Wrapper.transform);
    //
    ////            var scale =
    ////                RandomUtils.RandomVectorBetweenRange(
    ////                    level.Settings.NodeMinimumScale,
    ////                    level.Settings.NodeMaximumScale);
    ////
    //            var scale = new Vector3(3, 3, 3);
    //            cube.transform.localScale = scale;
    //
    //            cube.GetComponent<Collider>().enabled = false;
    //            var collider = node.Wrapper.AddComponent<SphereCollider>();
    //            collider.radius = new [] { scale.x, scale.y, scale.z }.Max() - 0.5f;
    //
    //            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //            sphere.transform.parent = node.Wrapper.transform;
    //            sphere.transform.localScale = new Vector3(collider.radius * 2, collider.radius * 2, collider.radius * 2);
    //
    //            sphere.GetComponent<Collider>().enabled = false;
    //
    //            return node;
    //        }
           
    //    }
}

