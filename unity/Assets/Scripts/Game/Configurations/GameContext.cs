using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using syscrawl.Game.Services.Levels;
using syscrawl.Game.Models.Levels;
using syscrawl.Game.Controllers.Levels;
using syscrawl.Game.Controllers.Player;
using syscrawl.Game.Models;
using syscrawl.Game.Views.Nodes;
using syscrawl.Game.Views.Levels;
using syscrawl.Game.Controllers;

namespace syscrawl.Game
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

            injectionBinder.Bind<ILevel>().To<Level>().ToSingleton();
            injectionBinder.Bind<IPlayer>().To<Player>().ToSingleton();

            commandBinder.Bind<GameStartSignal>().To<GameStartCommand>();
            commandBinder.Bind<GenerateLevelSignal>().To<GenerateLevelCommand>();
            commandBinder.Bind<PositionNodesSignal>().To<PositionNodesCommand>();
            commandBinder.Bind<CreateNodeSignal>().To<CreateNodeCommand>();
            commandBinder.Bind<PlayerMoveToSignal>().To<PlayerMoveToCommand>();

            mediationBinder.Bind<LevelView>().To<LevelMediator>();

            injectionBinder.Bind<PlayerMovedSignal>().ToSingleton();
            injectionBinder.Bind<LevelGeneratedSignal>().ToSingleton();
        }

        protected override void postBindings()
        {
            //Establish our camera. We do this early since it gets injected in places that help us do layout.
            UnityEngine.Camera cam = (contextView as GameObject).GetComponentInChildren<UnityEngine.Camera>();
            if (cam == null)
            {
                throw new Exception("GameContext couldn't find the game camera");
            }
            injectionBinder.Bind<UnityEngine.Camera>().ToValue(cam);
        }
    }
}

