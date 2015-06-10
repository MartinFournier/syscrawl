using System;
using UnityEngine;
using strange.extensions.mediation.impl;

namespace syscrawl.Game.Views.Nodes
{
    public class FilesystemNodeView : BaseNodeView
    {
        internal override void Init()
        {
            var cube1 = CreateCube();
            var cube2 = CreateCube();

            var margin = 0.25f;

            cube2.transform.Translate(
                cube2.transform.localScale.x + margin, 0, 0, cube1.transform);

            var cube3 = CreateCube();
            cube3.transform.Translate(
                0, 0, cube3.transform.localScale.z + margin, cube2.transform);

            var cube4 = CreateCube();
            cube4.transform.localPosition = cube3.transform.localPosition;
            cube4.transform.Translate(
                cube4.transform.localScale.x + margin, 0, 0, cube3.transform);

            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 2;
            collider.center = new Vector3(0.62f, 0f, 0.62f);
        }

        GameObject CreateCube()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(this.transform);
            cube.transform.localPosition = Vector3.zero;
            var material = Resources.Load<Material>("Materials/Nodes/Filesystem");
            var cubeRenderer = cube.GetComponent<Renderer>();
            cubeRenderer.material = material;

            cube.GetComponent<Collider>().enabled = false;

            return cube;
        }
    }
}

