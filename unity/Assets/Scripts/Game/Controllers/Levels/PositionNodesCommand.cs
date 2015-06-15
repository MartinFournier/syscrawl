using strange.extensions.command.impl;
using syscrawl.Game.Views.Levels;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game.Models;
using syscrawl.Game.Views.Nodes;
using syscrawl.Common.Extensions;
using System.Linq;
using System;
using syscrawl.Game.Services.Levels;

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

        [Inject]
        public INodePositionServices NodePositionServices { get; set; }

        public override void Execute()
        {
            var nodePositions = 
                NodePositionServices.GetPositions(
                    Level.GetGraph(), 
                    Player.CurrentNode, 
                    Player.PreviousNode);

            LevelMediator.RemoveNodes();

            PositionNodes(nodePositions);
        }

        void PositionNodes(NodePositions nodePositions)
        {
            foreach (var node in nodePositions.Keys)
            {
                var sceneNode = nodePositions[node];
                PositionNode(node, sceneNode, nodePositions);
            }
        }

        void PositionNode(
            Node node, 
            SceneNode sceneNode,
            NodePositions positions)
        {
            var container = 
                LevelMediator.GetNodeContainerForType(sceneNode.type);

            CreateNodeSignal.Dispatch(
                node, container, sceneNode.position, sceneNode.type);

            if (!IsNodeShowingConnections(sceneNode.type))
                return;

            var childSceneNodes = node.GetConnections();
            //TODO: Will cause duplicated lines
            foreach (var childSceneNode in childSceneNodes)
            {
                var childNode = positions[childSceneNode];
                DispatchNodeConnectionSignal(sceneNode, childNode, container);
            }
        }

        void DispatchNodeConnectionSignal(
            SceneNode parentNode,
            SceneNode childNode, 
            GameObject container)
        {
            var nodeData = new CreateNodeConnection
            {
                From = parentNode.position,
                To = childNode.position,
                Container = container
            };
            CreateNodeConnectionSignal.Dispatch(nodeData);
        }

        bool IsNodeShowingConnections(SceneNodeType type)
        {
            return
                type == SceneNodeType.Active ||
            type == SceneNodeType.Current;
        }
    }
}
