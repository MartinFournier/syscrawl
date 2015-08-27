using System;
using strange.extensions.command.impl;
using syscrawl.Game.Models.Levels;
using syscrawl.Common.Extensions;
using syscrawl.Game.Views.Nodes;
using UnityEngine;

namespace syscrawl.Game.Controllers.Levels
{
    public class CreateNodeConnectionCommand : Command
    {
        [Inject]
        public CreateNodeConnection NodeConnection { get; set; }

        public override void Execute()
        {
            var container = NodeConnection.Container;
            var connection = 
                container.CreateSubcomponent<NodeConnectionView>(
                    "Connection", Vector3.zero
                );

            connection.Init(NodeConnection.From, NodeConnection.To);
        }
    }
}

