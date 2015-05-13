using UnityEngine;
using syscrawl.Levels;
using syscrawl.Levels.Nodes;

namespace syscrawl.Player
{
    public class Player : MonoBehaviour
    {
        Level Level { get; set; }

        Node CurrentNode { get; set; }

        GameObject Entity { get; set; }

        void Start()
        {
//            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//            cube.GetComponent<Collider>().enabled = false;
//            cube.transform.SetParent(transform);
//            Entity = cube;

            gameObject.AddComponent<BoxCollider>();
        }

        void OnGUI()
        {
            GUI.TextArea(
                new Rect(10, 300, 200, 200),
                this.ToString());
        }

        public void SetLevel(Level level)
        {
            this.Level = level;
            this.CurrentNode = level.Graph.Entrance;

        }

        public override string ToString()
        {
            return string.Format("At Node {0}", CurrentNode);
        }

        void Update()
        {
            var pos = CurrentNode.Wrapper.transform.position;
            transform.position = new Vector3(pos.x, pos.y + 4, pos.z);
        }

        void OnMouseEnter()
        {
            Debug.Log("Player: MouseEnter");
        }

        void OnMouseExit()
        {
            Debug.Log("Player: MouseExit");
        }

        void OnMouseUp()
        {
            Debug.Log("Player: MouseUp");
        }

        void OnMouseDown()
        {
            Debug.Log("Player: MouseDown");
        }
    }
}