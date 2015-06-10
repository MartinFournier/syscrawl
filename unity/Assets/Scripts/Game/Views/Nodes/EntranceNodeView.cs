using strange.extensions.mediation.impl;
using UnityEngine;

namespace syscrawl.Game.Views.Nodes
{
    public class EntranceNodeView : BaseNodeView
    {
        internal override void Init()
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(gameObject.transform);

            var material = Resources.Load<Material>("Materials/Nodes/SphereNode");
            var renderer = sphere.GetComponent<Renderer>();

            renderer.material = material;

            var scale = new Vector3(3, 3, 3);

            sphere.transform.localScale = scale;

            sphere.GetComponent<Collider>().enabled = false;
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 2f;
        }
    }
}
