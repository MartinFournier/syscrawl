using UnityEngine;
using strange.extensions.mediation.impl;
using syscrawl.Signals;

namespace syscrawl.Views.Nodes
{
    public class NodeWrapperMediator : Mediator
    {
        [Inject]
        public NodeWrapperView View { get; set; }

        [Inject]
        public PlayerMovedSignal signal { get; set; }

        public override void OnRegister()
        {
            Debug.Log("Register in the meditor");
            signal.AddListener(Test);
            View.init();
        }

        void Test()
        {
            Debug.Log("Hi, player moved");
        }
    }
}

