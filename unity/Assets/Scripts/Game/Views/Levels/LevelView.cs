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
            activeNodes = 
                gameObject.CreateChildObject("Active Nodes", Vector3.zero);
            previousNodes = 
                gameObject.CreateChildObject("Previous Nodes", Vector3.zero);
            currentNodes = 
                gameObject.CreateChildObject("Current Nodes", Vector3.zero);
            furtherAheadNodes = 
                gameObject.CreateChildObject("FurtherAhead Nodes", Vector3.zero);
        }
    }
}

