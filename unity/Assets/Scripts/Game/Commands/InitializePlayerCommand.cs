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
            player.Name = "Booyha-Guy";
            var entrance = level.GetEntrance();
            player.MoveTo(entrance);
            Debug.Log("Command: Player has been moved");
        }
    }
}

