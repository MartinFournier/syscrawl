using UnityEngine;
using NGenerics.DataStructures.General;
using System.Linq;

namespace syscrawl.Levels.Nodes
{
    public abstract class Node : MonoBehaviour
    {
        public NodeType Type { get; set; }

        public GameObject Wrapper { get; set; }

        public Vertex<Node> Vertex { get; set; }

        protected static T Create<T>(
            Level level, 
            string nodeName,
            NodeType type) where T:Node
        {
            var nodeObject = new GameObject(nodeName);
            nodeObject.transform.parent = level.transform;
            var node = nodeObject.AddComponent<T>();

            node.Type = type;
            node.Wrapper = nodeObject;

            GameObject sphereFog = Instantiate(Resources.Load("Prefabs/SphereFog")) as GameObject;

            sphereFog.transform.parent = node.Wrapper.transform;
            sphereFog.transform.localScale = new Vector3(10, 10, 10);

            return node;
        }

        public void SetVisible(bool isVisible)
        {
            var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var r in renderers)
            {
                r.enabled = isVisible;
            }
        }

        protected void Start()
        {
            Debug.Log("Start NodeType: " + Type);
            SetVisible(false);
        }

        public override string ToString()
        {
            return string.Format("[Node: Type={0}, Wrapper={1}, Pos={2}]", Type, Wrapper, transform.position);
        }

        void OnMouseEnter()
        {
            Debug.Log("Node: Enter (" + Type + ":" + Wrapper.name + ")");
        }


        void OnMouseExit()
        {
            Debug.Log("Node: Node (" + Type + ":" + Wrapper.name + ")");
        }

        void OnMouseDown()
        {
            Debug.Log("Node: MouseDown (" + Type + ":" + Wrapper.name + ")");
        }

        void OnMouseUp()
        {
            Debug.Log("Node: MouseUp (" + Type + ":" + Wrapper.name + ")");
        }
    }
}