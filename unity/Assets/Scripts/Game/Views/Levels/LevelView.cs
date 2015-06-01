using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Views.Levels
{
    public class LevelView : View
    {
        internal GameObject previousNodes;
        internal GameObject currentNodes;
        internal GameObject activeNodes;
        internal GameObject furtherAheadNodes;

        internal void Init()
        {
            activeNodes = gameObject.AttachObject("Active Nodes");
            previousNodes = gameObject.AttachObject("Previous Nodes");
            currentNodes = gameObject.AttachObject("Current Nodes");
            furtherAheadNodes = gameObject.AttachObject("FurtheAhead Nodes");
        }
    }
}

