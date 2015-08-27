using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Extensions;
using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Views.Nodes
{
    public abstract class BaseNodeView : View
    {
        internal abstract void Init();

        public static T Create<T>(
            GameObject container, string name) where T:BaseNodeView
        {
            var view = 
                container.CreateSubcomponent<T>(name, Vector3.zero);

            view.Init();
            return view;
        }

        public static BaseNodeView Create(
            NodeType type, GameObject container, string name)
        {
            BaseNodeView view = null;
            switch (type)
            {
                case NodeType.Connector:
                    view = Create<ConnectorNodeView>(container, name);
                    break;
                case NodeType.Entrance:
                    view = Create<EntranceNodeView>(container, name);
                    break;
                case NodeType.Filesystem:
                    view = Create<FilesystemNodeView>(container, name);
                    break;
                case NodeType.Firewall:
                    view = Create<FirewallNodeView>(container, name);
                    break;
            }
            return view;
        }
    }
}

