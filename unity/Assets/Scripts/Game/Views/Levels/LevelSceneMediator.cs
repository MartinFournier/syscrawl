using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game;

namespace syscrawl.Game.Views.Levels
{
    public class LevelSceneMediator : Mediator
    {
        [Inject]
        public LevelSceneView View { get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSignal { get; set; }

        [Inject]
        public LevelGeneratedSignal LevelGeneratedSignal { get; set; }

        [Inject]
        public PlayerMovedSignal PlayerMovedSignal { get; set; }

        [Inject]
        public PositionNodesSignal PositionNodesSignal { get; set; }

        public override void OnRegister()
        {
            View.Init();

//            LevelGeneratedSignal.AddListener(PositionNodes);
            PlayerMovedSignal.AddListener(PositionNodes);

            GenerateLevelSignal.Dispatch();
        }

        void PositionNodes()
        {
            Debug.Log("Positioning nodes");
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
