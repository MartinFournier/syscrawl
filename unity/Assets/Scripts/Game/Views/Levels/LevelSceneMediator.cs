using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Game.Models.Levels;
using syscrawl.Game;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Views.Levels
{
    public class LevelSceneMediator : Mediator
    {
        [Inject]
        public LevelSceneView View { get; set; }

        [Inject]
        public GenerateLevelSignal GenerateLevelSceneSignal { get; set; }

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

            GenerateLevelSceneSignal.Dispatch();
        }

        void PositionNodes()
        {
            Debug.Log("Positioning nodes");
            PositionNodesSignal.Dispatch(this);
        }

        public void RemoveNodes()
        {
            View.activeNodes.DestroyChildren();
            View.currentNodes.DestroyChildren();
            View.furtherAheadNodes.DestroyChildren();
            View.previousNodes.DestroyChildren();
        }

        public GameObject GetNodeContainerForType(SceneNodeType type)
        {
            var container = View.activeNodes;
            switch (type)
            {
                case SceneNodeType.Active:
                    container = View.activeNodes;
                    break;
                case SceneNodeType.Current:
                    container = View.currentNodes;
                    break;
                case SceneNodeType.FurtherAhead:
                    container = View.furtherAheadNodes;
                    break;
                case SceneNodeType.Previous:
                    container = View.previousNodes;
                    break;

            }
            return container;
        }
    }
}
