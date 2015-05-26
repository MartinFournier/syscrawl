using UnityEngine;
using strange.extensions.command.impl;
using syscrawl.Models.Levels;

namespace syscrawl
{
    public class GenerateLevelCommand : Command
    {
        //
        //        [Inject(ContextKeys.CONTEXT_VIEW)]
        //        public GameObject contextView{ get; set; }

        readonly ILevel level;

        public GenerateLevelCommand(ILevel level)
        {
            this.level = level;
        }

        public override void Execute()
        {
            Debug.Log("Command: Level has been generated");
        }
    }
}

