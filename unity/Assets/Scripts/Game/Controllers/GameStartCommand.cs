using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;
using syscrawl.Game.Views.Levels;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Controllers
{
    public class GameStartCommand : Command
    {
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject ContextView{ get; set; }

        public override void Execute()
        {
            ContextView.CreateSubcomponent<LevelView>("Level", Vector3.zero);
        }
    }
}

