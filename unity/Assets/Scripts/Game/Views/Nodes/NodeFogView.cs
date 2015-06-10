using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeFogView : View
    {
        GameObject fog;

        internal void Init()
        {
            fog = Prefabs.Instantiate("SphereFog", gameObject);
            fog.transform.localScale = new Vector3(10, 10, 10);
            fog.GetComponent<Collider>().enabled = false;
        }
    }
}

