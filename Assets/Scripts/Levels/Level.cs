using UnityEngine;
using System.Collections;
using syscrawl.Levels.Graph;
using syscrawl.Levels.Graph.Generators;
using System.Linq;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;

namespace syscrawl.Levels
{
   
    public class Level : MonoBehaviour
    {
        public LevelSettings Settings { get; private set; }

        public LevelGraph Graph;

        Node previousNode;
        Node currentNode;

        void OnGUI()
        {
            if (Graph != null)
            {
                GUI.TextArea(
                    new Rect(10, 10, 200, 200), 
                    Graph.ToString());
            }
        }

        public void Generate(LevelSettings settings)
        {
            Settings = settings;
            
//             Graph = TestGraph.Generate(this, settings);
            Graph = SpecificGraph.Generate(this, settings);

            previousNode = Graph.Entrance;
            currentNode = 
                Graph.Entrance.
                Vertex.IncidentEdges[0].
                GetPartnerVertex(previousNode.Vertex).Data;


            var edges = currentNode.Vertex.IncidentEdges;
            var vertexes = 
                edges.
                Select(
                    x => x.GetPartnerVertex(currentNode.Vertex)
                ).
                Where(x => x != previousNode.Vertex);
            
            var nodes = vertexes.Select(x => x.Data).ToList();


            var q = new Queue<Node>(nodes);

            var angle = 90f;
            var distance = 50f;

            Node centerNode = null;
            if (q.Count() % 2 == 1)
            {
                centerNode = q.Dequeue(); 
            }

            var leftNodes = new List<Node>();
            var rightNodes = new List<Node>();

            while (q.Count > 0)
            {
                //will be even since we dequeue if uneven above
                leftNodes.Add(q.Dequeue());
                rightNodes.Add(q.Dequeue());
            }
           

            if (centerNode != null)
            {
                centerNode.transform.position = new Vector3(distance, 0, 0);
            }

            PositionNodes(leftNodes, rightNodes, distance, angle, centerNode != null);

            previousNode.transform.position = new Vector3(-distance, 0, 0);

        }

        void PositionNodes(
            IEnumerable<Node> leftNodes,
            IEnumerable<Node> rightNodes, 
            float distance, 
            float angle,
            bool hasCenter)
        {

            var point = new Vector3(distance, 0, 0);
            var pivot = new Vector3(0, 0, 0);

            RotateNodes(leftNodes, point, pivot, angle, true, hasCenter);
            RotateNodes(rightNodes, point, pivot, angle, false, hasCenter);
        }

        void RotateNodes(
            IEnumerable<Node> nodes, 
            Vector3 point, 
            Vector3 pivot, 
            float angle,
            bool negate,
            bool hasCenter)
        {
            var angleDiff = angle / nodes.Count();
            var remainingAngle = angleDiff;
            if (!hasCenter)
            {
                // We move the first off half an increment
                remainingAngle = remainingAngle - angleDiff / 2;
            }
            foreach (var node in nodes)
            {
                var actualAngle = negate ? -remainingAngle : remainingAngle;
                var angles = new Vector3(0, actualAngle, 0);
                node.transform.position = 
                    RotatePointAroundPivot(point, pivot, angles);
                
                remainingAngle += angleDiff;
            }
        }

        Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var direction = point - pivot;
            direction = Quaternion.Euler(angles) * direction;
            var finalVector = direction + pivot;
            return finalVector;
        }
    }
}