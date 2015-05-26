using strange.extensions.command.impl;
using UnityEngine;
using syscrawl.Models.Levels;


namespace syscrawl.Commands
{
    public class InitializePlayerCommand : Command
    {
        [Inject]
        public ILevel level { get; set; }

        public override void Execute()
        {
            Debug.Log("Hello from the player thing!");
            Debug.Log(level.ToString());
        }
    }
}

