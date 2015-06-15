using UnityEngine;
using strange.extensions.command.impl;
using syscrawl.Game.Models.Levels;
using strange.extensions.context.api;
using syscrawl.Game.Models;
using System.Linq;

namespace syscrawl.Game.Controllers.Levels
{
    public class GenerateLevelSceneCommand : Command
    {
        
        //        [Inject(ContextKeys.CONTEXT_VIEW)]
        //        public GameObject ContextView{ get; set; }

        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        [Inject]
        public LevelGeneratedSignal LevelGeneratedSignal { get; set; }

        public override void Execute()
        {
            Level.Generate("Level1");

            var entrance = Level.GetEntrance();
            //TODO: This is debug. 
            var p = Player as Models.Player;
            p.CurrentNode = entrance;
            var first = entrance.GetConnections().First();
            p.MoveTo(first);

            //Player.MoveTo(entrance);
      

            LevelGeneratedSignal.Dispatch();
        }
    }
}

