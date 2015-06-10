using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game;

namespace syscrawl.Game.Views.Levels
{
    public class LevelMediator : Mediator
    {
        [Inject]
        public LevelView View { get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSignal { get; set; }

        [Inject]
        public LevelGeneratedSignal LevelGeneratedSignal { get; set; }

        [Inject]
        public PositionNodesSignal PositionNodesSignal { get; set; }

        public override void OnRegister()
        {
            View.Init();

            LevelGeneratedSignal.AddListener(PositionNodes);
            GenerateLevelSignal.Dispatch();
        }

        void PositionNodes()
        {
            PositionNodesSignal.Dispatch(this);
        }

        public GameObject GetNodeContainerForType(GameNodeType type)
        {
            var container = View.activeNodes;
            switch (type)
            {
                case GameNodeType.Active:
                    container = View.activeNodes;
                    break;
                case GameNodeType.Current:
                    container = View.currentNodes;
                    break;
                case GameNodeType.FurtherAhead:
                    container = View.furtherAheadNodes;
                    break;
                case GameNodeType.Previous:
                    container = View.previousNodes;
                    break;

            }
            return container;
        }
    }
}
