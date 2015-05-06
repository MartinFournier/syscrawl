﻿using System;
using UnityEngine;
using syscrawl.Utils;
using System.Linq;

namespace syscrawl.Levels.Nodes
{
    public class ConnectorNode : Node
    {
        Renderer sphereRenderer;
        bool isRevealing = false;

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
            var renderer = sphere.GetComponent<Renderer>();
            var material = Resources.Load<Material>("Materials/Nodes/NodePixelCutout");
            renderer.material = material;

            node.sphereRenderer = renderer;
            return node;
        }

        bool isAnimating = true;

        void OnMouseEnter()
        {
            isRevealing = true;
            isAnimating = true;
        }

        void OnMouseExit()
        {
            isRevealing = false;
            isAnimating = true;
        }

        void Update()
        {
            if (sphereRenderer == null)
                return;

            if (isAnimating)
            {
                var current = sphereRenderer.material.GetFloat("_Cutoff");
                var valueTo = 0f;

                if (isRevealing)
                {
                    valueTo = 1;
                }
                else
                {
                    valueTo = 0;
                }

                var value = Mathf.Lerp(current, valueTo, Time.deltaTime * 2);
                if (value < 0.01)
                {
                    value = 0;
                }
                if (value > 0.99)
                {
                    value = 1;
                }

                if (value == 0 || value == 1)
                {
                    isAnimating = false;
                }

                Debug.Log("Lerping " + current + " to " + valueTo + " with " + value);
                sphereRenderer.material.SetFloat("_Cutoff", value);
            }
        }
    }
}

