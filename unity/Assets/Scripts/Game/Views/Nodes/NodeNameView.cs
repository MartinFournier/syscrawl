using System;
using strange.extensions.mediation.impl;
using syscrawl.Common.Extensions;
using UnityEngine;
using syscrawl.Common.Utils;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeNameView : View
    {
        GameObject nodeName;
        TextMesh nodeNameMesh;

        internal void Init(string name)
        {
            nodeName = Prefabs.Instantiate("NodeName", gameObject);
            nodeNameMesh = nodeName.GetComponent<TextMesh>();
            nodeNameMesh.text = name;
        }
    }
}

