using UnityEngine;
using strange.extensions.mediation.impl;
using syscrawl.Signals;
using strange.extensions.context.api;

namespace syscrawl.Views.Nodes
{
    public class NodeWrapperMediator : Mediator
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView{ get; set; }

        [Inject]
        public NodeWrapperView View { get; set; }

        [Inject]
        public PlayerMovedSignal Signal { get; set; }

        public override void OnRegister()
        {
            Debug.Log("Register in the meditor");
            Signal.AddListener(Test);
            View.Init();
        }

        void Test()
        {
            Debug.Log("Hi, player moved");
        }
    }
}

