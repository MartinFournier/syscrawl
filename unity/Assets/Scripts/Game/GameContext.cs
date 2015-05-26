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
            //Injection binding.
            //Map a mock model and a mock service, both as Singletons
//			injectionBinder.Bind<IExampleModel>().To<ExampleModel>().ToSingleton();
//			injectionBinder.Bind<IExampleService>().To<ExampleService>().ToSingleton();
//
//			//View/Mediator binding
//			//This Binding instantiates a new ExampleMediator whenever as ExampleView
//			//Fires its Awake method. The Mediator communicates to/from the View
//			//and to/from the App. This keeps dependencies between the view and the app
//			//separated.
//            mediationBinder.Bind<TestView>().To<TestViewMediator>();
//
//			//Event/Command binding
//			commandBinder.Bind(ExampleEvent.REQUEST_WEB_SERVICE).To<CallWebServiceCommand>();
//			//The START event is fired as soon as mappings are complete.
//			//Note how we've bound it "Once". This means that the mapping goes away as soon as the command fires.

            commandBinder.Bind<GameStartSignal>().To<GenerateLevelCommand>().Once();

        }

    }
}

