using System;
using UnityEngine;
using syscrawl.Utils;
using System.Linq;

namespace syscrawl.Levels.Nodes
{
    public class ConnectorNode : Node
    {
        public static ConnectorNode Create(
            Level level, string nodeName)
        {
            var node = 
                Node.Create<ConnectorNode>(
                    level, nodeName, NodeType.Connector);

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(node.Wrapper.transform);

            var scale = 
                RandomUtils.RandomVectorBetweenRange(
                    level.Settings.NodeMinimumScale, 
                    level.Settings.NodeMaximumScale);

            cube.transform.localScale = scale;

            cube.GetComponent<Collider>().enabled = false;
            var collider = node.Wrapper.AddComponent<SphereCollider>();
            collider.radius = new [] { scale.x, scale.y, scale.z }.Max() - 0.5f;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = node.Wrapper.transform;
            sphere.transform.localScale = new Vector3(collider.radius * 2, collider.radius * 2, collider.radius * 2);

            sphere.GetComponent<Collider>().enabled = false;

            return node;
        }
           
    }
}

