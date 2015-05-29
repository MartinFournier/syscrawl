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
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        [Inject]
        public GenerateLevelSignal LevelGeneratedSignal { get; set; }

        public override void OnRegister()
        {
            Debug.Log("OnRegister in Level");
            View.Init();

            LevelGeneratedSignal.AddListener(Thing);
        }

        void Thing()
        {

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
                GameObject container = View.activeNodes;
                switch (node.type)
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

                //                var o = ContextView.injectionBinder.GetInstance<NodeWrapperView>();

                var nodeView = 
                    container.AttachSubcomponent<NodeWrapperView>(key.Name);
                nodeView.transform.position = node.position;
                var n = nodeView.GetComponent<NodeWrapperView>();
                n.SetName(key.Name);
                //                nodeView.Name
        
            }
        }
    }
}
