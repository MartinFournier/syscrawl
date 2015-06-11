using strange.extensions.command.impl;
using syscrawl.Game.Views.Levels;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game.Models;
using syscrawl.Game.Views.Nodes;
using syscrawl.Common.Extensions;
using System.Linq;
using System;

namespace syscrawl.Game.Controllers.Levels
{
    public class PositionNodesCommand : Command
    {
        [Inject]
        public LevelSceneMediator LevelMediator{ get; set; }


        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }


        [Inject]
        public CreateNodeSignal CreateNodeSignal { get; set; }

        [Inject]
        public CreateNodeConnectionSignal CreateNodeConnectionSignal { get; set; }

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
                CreateNodeSignal.Dispatch(key, container, node.position, node.type);

                if (
                    node.type == SceneNodeType.Active ||
                    node.type == SceneNodeType.Current)
                {
                    var connections = key.GetConnections();
                    Debug.Log(connections.Count() + " connections for " + key.ToString());
                    //TODO: Will cause duplicated lines
                    foreach (var connection in connections)
                    {
                        var data = new CreateNodeConnection
                        {
                            From = node.position,
                            To = nodePositions[connection].position,
                            Container = container
                        };
                        Debug.Log(String.Format("Line from {0} to {1}", data.From, data.To));
                        CreateNodeConnectionSignal.Dispatch(data);
                    }
                }
                   
            }
        }
    }
}
