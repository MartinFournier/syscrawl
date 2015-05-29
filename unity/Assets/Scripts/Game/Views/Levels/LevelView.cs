using System;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace syscrawl.Views.Levels
{
    public class LevelView : View
    {
        GameObject sublevelThing;

        internal void init()
        {
            sublevelThing = new GameObject("Sublevel thing");
            sublevelThing.transform.parent = gameObject.transform;
        }
            
    }
}

