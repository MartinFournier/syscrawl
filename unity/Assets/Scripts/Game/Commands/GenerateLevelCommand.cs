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

        //        [Inject]
        public ILevel level { get; set; }

        //        [Inject]
        public LevelGeneratedSignal signal { get; set; }

        public GenerateLevelCommand(ILevel level, LevelGeneratedSignal signal)
        {
            this.level = level;
            this.signal = signal;
        }

        public override void Execute()
        {
            Debug.Log(level.ToString());
            Debug.Log("Command: Level has been generated");
//            var testThing = injectionBinder.GetInstance<LevelGeneratedSignal>();
//            testThing.Dispatch(level);
            signal.Dispatch(level as Level); // Meh. don't get why we can't use an interface
        }
    }
}

