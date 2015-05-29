using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Models.Levels;
using syscrawl.Models;
using syscrawl.Views.Nodes;
using syscrawl.Extensions;
using System.Linq;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using syscrawl.Signals;

namespace syscrawl.Views.Levels
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
            Debug.Log("OnRegister in LevelMediator");

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
