using strange.extensions.mediation.impl;

namespace syscrawl.Game.Views.Nodes
{
    public class FirewallNodeMediator : Mediator
    {
        [Inject]
        public FirewallNodeView View { get; set; }
    }
}

