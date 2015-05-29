using System;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using syscrawl.Views.Levels;
using syscrawl.Extensions;
using syscrawl.Signals;

namespace syscrawl.Commands
{
    public class GameStartCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView{ get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSignal { get; set; }

        public override void Execute()
        {
            ContextView.AttachSubcomponent<LevelView>("Level");
            GenerateLevelSignal.Dispatch();
        }
    }
}

