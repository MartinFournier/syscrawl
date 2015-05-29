using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Models.Levels;
using syscrawl.Models;

namespace syscrawl.Views.Levels
{
    public class LevelMediator : Mediator
    {
        [Inject]
        public LevelView View { get; set; }

        [Inject]
        public ILevel Level { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        public override void OnRegister()
        {
            Debug.Log("Level mediator");
            View.init();
        }
    }
}

