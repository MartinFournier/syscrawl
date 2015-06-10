using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;
using syscrawl.Game.Models.Levels;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeWrapperView : View
    {
        public Node Node { get; private set; }

        GameObject wrapper;
        NodeFogView fog;
        NodeNameView nodeName;
        BaseNodeView view;

        internal void Init(Node node)
        {
            Node = node;

            wrapper = Prefabs.Instantiate("NodeWrapper", gameObject);

            fog = 
                wrapper.CreateSubcomponent<NodeFogView>("Fog", Vector3.zero);
            fog.Init();

            nodeName = 
                wrapper.CreateSubcomponent<NodeNameView>("Name", Vector3.zero);
            nodeName.Init(Node.Name);

            view = BaseNodeView.Create(node.type, wrapper, "View");
        }


        void OnMouseEnter()
        {
            //            Debug.Log("Node: Enter (" + Type + ":" + Wrapper.name + ")");
        }


        void OnMouseExit()
        {
            //            Debug.Log("Node: Node (" + Type + ":" + Wrapper.name + ")");
        }

        void OnMouseDown()
        {
            //            Debug.Log("Node: MouseDown (" + Type + ":" + Wrapper.name + ")");
        }

        void OnMouseUp()
        {
            Debug.Log("Node: MouseUp (" + "TYPE SHOULD BE HERE" + ":" + wrapper.name + ")");
            //            Level.Positioning.MoveTo(this);
        }

        public override string ToString()
        {
            return string.Format(
                "[Node: Type={0}, Wrapper={1}, Pos={2}]", "Type", 
                wrapper, transform.position);
        }
    }
}
