using strange.extensions.command.impl;
using syscrawl.Game.Views.Levels;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game.Models;
using syscrawl.Game.Views.Nodes;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Controllers.Levels
{
    public class PositionNodesCommand : Command
    {
        [Inject]
        public LevelMediator LevelMediator{ get; set; }


        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }


        [Inject]
        public CreateNodeSignal CreateNodeSignal { get; set; }

        public override void Execute()
        {
            var nodePositions = 
                new NodePositions(
                    Level.GetGraph(),
                    Player.CurrentNode,
                    Player.PreviousNode,
                    90f, 20f
                );
            foreach (var key in nodePositions.Keys)
            {
                var node = nodePositions[key];
                var container = LevelMediator.GetNodeContainerForType(node.type);
                CreateNodeSignal.Dispatch(key, container, node.position);
            }
        }
    }
}
