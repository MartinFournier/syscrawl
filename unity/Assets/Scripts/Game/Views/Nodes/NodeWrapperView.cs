using System;
using strange.extensions.mediation.impl;
using UnityEngine;
using syscrawl.Utils;

namespace syscrawl.Views.Nodes
{
    public class NodeWrapperView : View
    {
        //        public NodeType Type { get; private set; }

        /// <summary>
        /// Uses the wrapper's scale as a helper
        /// </summary>
        /// <value>The scale.</value>
        public Vector3 Scale
        {
            get
            {
                return transform.localScale;
            }
            set
            {
                transform.localScale = value;
            }
        }

        /// <summary>
        /// Uses the wrapper's global position as a helper
        /// </summary>
        /// <value>The position.</value>
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        //        public void SetVisible(bool isVisible)
        //        {
        //            //TODO: this is a temp hack.
        //            var renderers =
        //                gameObject.
        //                GetComponentsInChildren<MeshRenderer>().
        //                Where(x => !x.name.Equals("SphereFog(Clone)"));
        //
        //            foreach (var r in renderers)
        //            {
        //                r.enabled = isVisible;
        //            }
        //        }

        internal GameObject wrapper;
        internal GameObject fog;
        internal GameObject nodeName;
        internal TextMesh nodeNameMesh;

        internal void Init()
        {
            wrapper = Prefabs.Instantiate("NodeWrapper");
            wrapper.transform.parent = gameObject.transform;

            fog = Prefabs.Instantiate("SphereFog");
            fog.transform.parent = wrapper.transform;
            fog.transform.localScale = new Vector3(10, 10, 10);
            fog.GetComponent<Collider>().enabled = false;

            nodeName = Prefabs.Instantiate("NodeName");
            nodeNameMesh = nodeName.GetComponent<TextMesh>();
            nodeName.transform.parent = wrapper.transform;
        }

        public void SetName(string name)
        {
            nodeNameMesh.text = name;
        }



        //        protected static T Create<T>(
        //            string nodeName,
        //            NodeType type) where T:Node
        //        {
        //            var nodeObject = new GameObject(nodeName);
        //            //            nodeObject.transform.parent = level.transform;
        //            var node = nodeObject.AddComponent<T>();
        //            //            node.Level = level;
        //
        //            var wrapper = Prefabs.Instantiate("NodeWrapper");
        //            var sphereFog = Prefabs.Instantiate("SphereFog");
        //
        //            sphereFog.transform.parent = node.Wrapper.transform;
        //            sphereFog.transform.localScale = new Vector3(10, 10, 10);
        //
        //            sphereFog.GetComponent<Collider>().enabled = false;
        //
        //            var nodeNamePrefab = Instantiate(Resources.Load("Prefabs/NodeName")) as GameObject;
        //            var textMesh = nodeNamePrefab.GetComponent<TextMesh>();
        //            textMesh.text = nodeName;
        //            nodeNamePrefab.transform.parent = node.Wrapper.transform;
        //
        //            return node;
        //        }



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

        //        protected void Start()
        //        {
        //
        //        }

        public override string ToString()
        {
            return string.Format("[Node: Type={0}, Wrapper={1}, Pos={2}]", "Type", wrapper, transform.position);
        }
    }
}
