using System;
using strange.extensions.context.impl;
using UnityEngine;

namespace syscrawl.Game
{
    public class GameRoot : ContextView
    {
        void Awake()
        {
            context = new GameContext(this);
        }
    }
}

