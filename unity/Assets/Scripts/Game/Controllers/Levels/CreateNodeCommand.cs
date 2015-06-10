using System;
using strange.extensions.command.impl;
using syscrawl.Game.Models.Levels;
using UnityEngine;
using syscrawl.Game.Views.Nodes;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Controllers.Levels
{
    public class CreateNodeCommand : Command
    {
        [Inject]
        public Node Node { get; set; }

        [Inject]
        public GameObject Container { get; set; }

        [Inject]
        public Vector3 Position { get; set; }

        public override void Execute()
        {
            var nodeWrapperView = 
                Container.CreateSubcomponent<NodeWrapperView>(
                    Node.Name, Position
                );
            
            nodeWrapperView.Init(Node);
        }

    }
}

