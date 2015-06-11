using System;
using strange.extensions.command.impl;
using syscrawl.Game.Models;
using syscrawl.Game.Models.Levels;
using UnityEngine;

namespace syscrawl.Game.Controllers.Player
{
    public class PlayerMoveToCommand : Command
    {
        [Inject]
        public IPlayer Player { get; set; }

        [Inject]
        public Node Node { get; set; }

        public override void Execute()
        {
            Debug.Log("Move to");
            Player.MoveTo(Node);
        }
    }
}

