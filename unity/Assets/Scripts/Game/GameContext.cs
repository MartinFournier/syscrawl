using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using syscrawl.Signals;
using syscrawl.Services.Levels;
using syscrawl.Models.Levels;
using syscrawl.Commands;

namespace syscrawl
{
    public class GameContext : MVCSContext
    {
        public GameContext(MonoBehaviour view)
            : base(view)
        {
        }

        public GameContext(MonoBehaviour view, ContextStartupFlags flags)
            : base(view, flags)
        {
        }

        // Unbind the default EventCommandBinder and rebind the SignalCommandBinder
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        // Override Start so that we can fire the StartSignal
        override public IContext Start()
        {
            base.Start();

            var startSignal = injectionBinder.GetInstance<GameStartSignal>();
            startSignal.Dispatch();

            return this;
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<ILevelGenerator>().To<SpecificLevelGenerator>();
            injectionBinder.Bind<ILevel>().To<Level>();
          
            commandBinder.Bind<GameStartSignal>().To<GenerateLevelCommand>().Once();
            commandBinder.Bind<LevelGeneratedSignal>().To<InitializePlayerCommand>();

        }

    }
}

