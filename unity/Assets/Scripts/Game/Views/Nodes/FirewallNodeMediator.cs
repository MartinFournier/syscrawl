using System;
using strange.extensions.mediation.impl;

namespace syscrawl.Views.Nodes
{
    public class FirewallNodeMediator : Mediator
    {
        [Inject]
        public FirewallNodeView View { get; set; }
    }
}

