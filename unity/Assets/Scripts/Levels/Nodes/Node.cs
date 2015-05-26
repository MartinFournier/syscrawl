using UnityEngine;
using NGenerics.DataStructures.General;
using System.Linq;
using System.Collections.Generic;
using syscrawl.Models.Levels;

namespace syscrawl.Levels.Nodes
{
    //    public abstract class NodeV : MonoBehaviour
    //    {
    //        #region Private & Protected Members
    //
    //        protected GameObject Wrapper { get; set; }
    //
    //        //        Level Level { get; set; }
    //
    //        // todo
    //        bool IsUncovered { get; set; }
    //
    //        #endregion
    //
    //        #region Public Members
    //
    //        public Vertex<Node> Vertex { get; set; }
    //
    //        public NodeType Type { get; private set; }
    //
    //        #endregion
    //
    //        #region Node Connections
    //
    //        public IEnumerable<Node> GetConnections()
    //        {
    //
    //            var nodes =
    //                Vertex.IncidentEdges.
    //                Select(
    //                    x => x.GetPartnerVertex(Vertex));
    //            return nodes.Select(x => x.Data);
    //        }
    //
    //        public IEnumerable<Node> GetConnections(
    //            params Node[] excludedConnections)
    //        {
    //            var nodes = GetConnections();
    //            nodes = nodes.Where(x => !excludedConnections.Contains(x));
    //            return  nodes;
    //        }
    //
    //        #endregion
    //
    //        #region Node Wrapper Properties
    //
    //        /// <summary>
    //        /// Uses the wrapper's scale as a helper
    //        /// </summary>
    //        /// <value>The scale.</value>
    //        public Vector3 Scale
    //        {
    //            get
    //            {
    //                return Wrapper.transform.localScale;
    //            }
    //            set
    //            {
    //                Wrapper.transform.localScale = value;
    //            }
    //        }
    //
    //        /// <summary>
    //        /// Uses the wrapper's global position as a helper
    //        /// </summary>
    //        /// <value>The position.</value>
    //        public Vector3 Position
    //        {
    //            get
    //            {
    //                return Wrapper.transform.position;
    //            }
    //            set
    //            {
    //                Wrapper.transform.position = value;
    //            }
    //        }
    //
    //        #endregion
    //
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
    //
    //        #region Factory
    //
    //        protected static T Create<T>(
    //            string nodeName,
    //            NodeType type) where T:Node
    //        {
    //            var nodeObject = new GameObject(nodeName);
    ////            nodeObject.transform.parent = level.transform;
    //            var node = nodeObject.AddComponent<T>();
    ////            node.Level = level;
    //
    //            node.Type = type;
    //            node.Wrapper = nodeObject;
    //
    //            var sphereFog = Instantiate(Resources.Load("Prefabs/SphereFog")) as GameObject;
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
    //
    //        #endregion
    //
    //        #region Mouse Events
    //
    //        void OnMouseEnter()
    //        {
    ////            Debug.Log("Node: Enter (" + Type + ":" + Wrapper.name + ")");
    //        }
    //
    //
    //        void OnMouseExit()
    //        {
    ////            Debug.Log("Node: Node (" + Type + ":" + Wrapper.name + ")");
    //        }
    //
    //        void OnMouseDown()
    //        {
    ////            Debug.Log("Node: MouseDown (" + Type + ":" + Wrapper.name + ")");
    //        }
    //
    //        void OnMouseUp()
    //        {
    //            Debug.Log("Node: MouseUp (" + Type + ":" + Wrapper.name + ")");
    ////            Level.Positioning.MoveTo(this);
    //        }
    //
    //        #endregion
    //
    //        protected void Start()
    //        {
    //
    //        }
    //
    //        public override string ToString()
    //        {
    //            return string.Format("[Node: Type={0}, Wrapper={1}, Pos={2}]", Type, Wrapper, transform.position);
    //        }
    //    }
}