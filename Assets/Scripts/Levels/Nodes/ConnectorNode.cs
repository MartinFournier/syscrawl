using System;
using UnityEngine;
using syscrawl.Utils;

namespace syscrawl.Levels.Nodes
{
    public class ConnectorNode : Node
    {
        public static ConnectorNode Create(
            Level level, string nodeName)
        {
            var nodeObject = new GameObject(nodeName);
            nodeObject.transform.parent = level.transform;
            var node = nodeObject.AddComponent<ConnectorNode>();

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(nodeObject.transform);

            var scale = 
                RandomUtils.RandomVectorBetweenRange(
                    level.Settings.NodeMinimumScale, 
                    level.Settings.NodeMaximumScale);

            cube.transform.localScale = scale;

            node.Wrapper = nodeObject;
            return node;
        }
    }
}

