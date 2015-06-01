using UnityEngine;
using strange.extensions.mediation.impl;
using syscrawl.Game;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeWrapperMediator : Mediator
    {
        [Inject]
        public NodeWrapperView View { get; set; }

        [Inject]
        public PlayerMovedSignal Signal { get; set; }

        public override void OnRegister()
        {
            View.Init();

            Signal.AddListener(Test);
        }

        void Test()
        {
            Debug.Log("Hi, player moved");
        }
    }
}

