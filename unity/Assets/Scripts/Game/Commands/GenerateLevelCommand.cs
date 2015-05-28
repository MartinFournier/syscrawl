using UnityEngine;
using strange.extensions.command.impl;
using syscrawl.Models.Levels;
using syscrawl.Signals;

namespace syscrawl.Commands
{
    public class GenerateLevelCommand : Command
    {
        //
        //        [Inject(ContextKeys.CONTEXT_VIEW)]
        //        public GameObject contextView{ get; set; }


        [Inject]
        public ILevel level { get; set; }

        [Inject]
        public LevelGeneratedSignal levelGeneratedSignal { get; set; }

        public override void Execute()
        {
            level.Generate("YAY PARTY");
            Debug.Log(level.ToString());
            Debug.Log("Command: Level has been generated");
            levelGeneratedSignal.Dispatch();
        }
    }
}

