using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;
using syscrawl.Game.Models.Levels;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeWrapperView : View
    {
        [Inject]
        public PlayerMoveToSignal PlayerMoveToSignal { get; set; }

        public Node Node { get; private set; }

        GameObject wrapper;
        NodeFogView fog;
        NodeNameView nodeName;
        BaseNodeView view;

        internal void Init(Node node, SceneNodeType nodeType)
        {
            Node = node;

            wrapper = Prefabs.Instantiate("NodeWrapper", gameObject);

            fog = 
                wrapper.CreateSubcomponent<NodeFogView>("Fog", Vector3.zero);
            fog.Init();

            nodeName = 
                wrapper.CreateSubcomponent<NodeNameView>("Name", Vector3.zero);
            nodeName.Init(Node.Name);

            //We need a collider on this gameObject if we want to handle Mouse Events
            var collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = 3f; // TODO: Find a correct bounding box.

            view = BaseNodeView.Create(node.type, wrapper, "View");

            if (
                nodeType == SceneNodeType.FurtherAhead ||
                nodeType == SceneNodeType.Previous)
            {

                wrapper.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
            else if (nodeType == SceneNodeType.Active)
            {
                wrapper.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
        }


        void OnMouseEnter()
        {
            Debug.Log("Node: Enter (" + Node.type + ":" + wrapper.name + ")");
            fog.HideSphere();
        }


        void OnMouseExit()
        {
            Debug.Log("Node: Node (" + Node.type + ":" + wrapper.name + ")");
            fog.ShowSphere();
        }

        void OnMouseDown()
        {
            Debug.Log("Node: MouseDown (" + Node.type + ":" + wrapper.name + ")");
        }

        void OnMouseUp()
        {
            Debug.Log("Node: MouseUp (" + Node.type + ":" + wrapper.name + ")");
            PlayerMoveToSignal.Dispatch(Node);
        }

        public override string ToString()
        {
            return string.Format(
                "[Node: Type={0}, Wrapper={1}, Pos={2}]", "Type", 
                wrapper, transform.position);
        }
    }
}
