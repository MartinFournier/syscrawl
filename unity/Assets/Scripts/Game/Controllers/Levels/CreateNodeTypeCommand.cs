using System;
using strange.extensions.command.impl;
using syscrawl.Game.Models.Levels;
using UnityEngine;

namespace syscrawl.Game.Controllers.Levels
{
    public class CreateNodeTypeCommand : Command
    {
        [Inject]
        public NodeType Type { get; set; }

        [Inject]
        public GameObject Container { get; set; }

        public override void Execute()
        {
            
        }
    }
}

