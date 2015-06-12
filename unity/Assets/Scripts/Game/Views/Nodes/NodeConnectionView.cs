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

            if (from.y == 0)
                from.y += 0.1f;

            if (to.y == 0)
                to.y += 0.1f;

            line.SetPosition(0, from);
            line.SetPosition(1, to);
        }
    }
}

