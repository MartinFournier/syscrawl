using UnityEngine;
using strange.extensions.mediation.impl;
using syscrawl.Signals;
using strange.extensions.context.api;

namespace syscrawl.Views.Nodes
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

