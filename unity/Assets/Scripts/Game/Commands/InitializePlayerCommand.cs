using strange.extensions.command.impl;
using UnityEngine;
using syscrawl.Models.Levels;
using syscrawl.Models;


namespace syscrawl.Commands
{
    public class InitializePlayerCommand : Command
    {
        [Inject]
        public ILevel level { get; set; }

        [Inject]
        public IPlayer player { get; set; }

        public override void Execute()
        {
            Debug.Log("Hello from the player thing!");
            player.Name = "Booyha-Guy";
            Debug.Log(level.ToString());
            player.MoveTo(level.GetEntrance());
        }
    }
}

