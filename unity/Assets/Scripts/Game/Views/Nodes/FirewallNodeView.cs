using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace syscrawl.Views.Nodes
{
    public class FirewallNodeView : View
    {
        GameObject Cube { get; set; }

        void Update()
        {
            Cube.transform.Rotate(0, 30 * Time.deltaTime, 0);
        }

        internal void Init()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = gameObject.transform;

            var material = Resources.Load<Material>("Materials/Nodes/Firewall");
            var renderer = cube.GetComponent<Renderer>();
            renderer.material = material;

            var scale = new Vector3(1, 4, 4);

            cube.GetComponent<Collider>().enabled = false;
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 3;

            cube.transform.localScale = scale;

            Cube = cube;
        }

    }
}

