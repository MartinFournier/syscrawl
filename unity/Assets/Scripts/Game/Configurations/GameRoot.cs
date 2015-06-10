using System;
using strange.extensions.context.impl;

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

