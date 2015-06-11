using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeConnectionView : View
    {
        GameObject connection;
        LineRenderer line;

        public void Init(Vector3 from, Vector3 to)
        {
            connection = Prefabs.Instantiate("NodeConnection", gameObject);
            line = connection.GetComponent<LineRenderer>();
            line.SetPosition(0, from);
            line.SetPosition(1, to);
        }
    }
}

