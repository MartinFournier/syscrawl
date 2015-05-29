﻿using UnityEngine;
using strange.extensions.command.impl;
using syscrawl.Models.Levels;
using syscrawl.Signals;
using strange.extensions.context.api;
using syscrawl.Views.Nodes;
using syscrawl.Views.Levels;
using syscrawl.Extensions;

namespace syscrawl.Commands
{
    public class GenerateLevelCommand : Command
    {
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView{ get; set; }

        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSignal { get; set; }


        public override void Execute()
        {
            Level.Generate("Level1");

            Debug.Log("Command: Level has been generated");
            GenerateLevelSignal.Dispatch();
        }
    }
}

