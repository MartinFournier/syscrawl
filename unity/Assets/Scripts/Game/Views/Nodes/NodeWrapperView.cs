using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Common.Utils;
using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Views.Nodes
{
    public class NodeWrapperView : View
    {
        public Node Node { get; private set; }

        internal GameObject wrapper;
        internal GameObject fog;
        internal GameObject nodeName;
        internal TextMesh nodeNameMesh;

        internal void Init(Node node)
        {
            Node = node;

            wrapper = Prefabs.Instantiate("NodeWrapper");
            wrapper.transform.parent = gameObject.transform;
            wrapper.transform.localPosition = Vector3.zero;

            fog = Prefabs.Instantiate("SphereFog");
            fog.transform.parent = wrapper.transform;
            fog.transform.localPosition = Vector3.zero;
            fog.transform.localScale = new Vector3(10, 10, 10);
            fog.GetComponent<Collider>().enabled = false;

            nodeName = Prefabs.Instantiate("NodeName");
            nodeNameMesh = nodeName.GetComponent<TextMesh>();
            nodeName.transform.parent = wrapper.transform;
            nodeName.transform.localPosition = Vector3.zero;

            nodeNameMesh.text = Node.Name;
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
