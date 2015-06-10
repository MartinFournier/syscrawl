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
            var nodeViewGameObject = 
                Container.AttachSubcomponent<NodeWrapperView>(Node.Name);
            nodeViewGameObject.transform.localPosition = Position;

            var nodeWrapperView = nodeViewGameObject.GetComponent<NodeWrapperView>();
            nodeWrapperView.Init(Node);
        }

    }
}

