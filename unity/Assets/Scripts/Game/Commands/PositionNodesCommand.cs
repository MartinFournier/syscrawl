using strange.extensions.command.impl;
using syscrawl.Views.Levels;
using UnityEngine;
using syscrawl.Models.Levels;
using syscrawl.Models;
using syscrawl.Views.Nodes;
using syscrawl.Extensions;

namespace syscrawl.Commands
{
    public class PositionNodesCommand : Command
    {
        [Inject]
        public LevelMediator LevelMediator{ get; set; }


        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        public override void Execute()
        {
            Debug.Log("Hi from the positioning command");
            Debug.Log("Callback from levels");
            var nodePositions = 
                new NodePositions(
                    Level.GetGraph(),
                    Player.CurrentNode,
                    Player.PreviousNode,
                    90f, 5f
                );
            foreach (var key in nodePositions.Keys)
            {
                var node = nodePositions[key];
                var container = LevelMediator.GetNodeContainerForType(node.type);

                var nodeView = 
                    container.AttachSubcomponent<NodeWrapperView>(key.Name);
                
                nodeView.transform.position = node.position;
                var n = nodeView.GetComponent<NodeWrapperView>();
                n.SetName(key.Name);
            }
        }
    }
}
