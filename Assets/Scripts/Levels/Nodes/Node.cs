using UnityEngine;
using NGenerics.DataStructures.General;

namespace syscrawl.Levels.Nodes
{
    public abstract class Node : MonoBehaviour
    {
        public GameObject Wrapper { get; set; }

        public Vertex<Node> Vertex { get; set; }
    }
}