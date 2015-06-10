using strange.extensions.mediation.impl;
using UnityEngine;
using System.Linq;

namespace syscrawl.Game.Views.Nodes
{
    public class ConnectorNodeView : View
    {
        internal void Init()
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(gameObject.transform);

            var scale = new Vector3(3, 3, 3);
            cube.transform.localScale = scale;

            cube.GetComponent<Collider>().enabled = false;
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = new [] { scale.x, scale.y, scale.z }.Max() - 0.5f;

            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = gameObject.transform;
            sphere.transform.localScale = new Vector3(collider.radius * 2, collider.radius * 2, collider.radius * 2);

            sphere.GetComponent<Collider>().enabled = false;
        }
    }
}
