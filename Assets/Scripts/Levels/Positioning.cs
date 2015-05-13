using System;
using UnityEngine;
using syscrawl.Levels.Nodes;
using System.Collections.Generic;
using System.Linq;
using syscrawl.Levels.Graph;
using syscrawl.Extensions;

namespace syscrawl.Levels
{
    public class Positioning
    {
        readonly float angle;
     
        readonly Vector3 pivotPosition = new Vector3(0, 0, 0);
        readonly Vector3 centerNodePosition;
        readonly Vector3 previousNodePosition;
        readonly NodesGraph graph;

        Node PreviousNode { get; set; }

        Node CurrentNode { get; set; }

        Vector3 Pivot
        {
            get { return CurrentNode.transform.localPosition; }
        }

        IEnumerable<Node> PartnerNodes
        {
            get
            {
                var nodes = 
                    CurrentNode.Vertex.IncidentEdges.
                    Select(
                        x => x.GetPartnerVertex(CurrentNode.Vertex));

                if (PreviousNode != null)
                {
                    nodes = nodes.Where(x => x != PreviousNode.Vertex);
                }
                return nodes.Select(x => x.Data);
            }
        }

        IEnumerable<Node> ActiveNodes
        {
            get
            { 
                var nodes = PartnerNodes.ToList();
                nodes.Add(CurrentNode);
                if (PreviousNode != null)
                    nodes.Add(PreviousNode);

                return nodes;
            }
        }

        public Positioning(
            NodesGraph graph, 
            float angle, 
            float distance)
        {

            this.graph = graph;
            this.angle = angle;

            centerNodePosition = new Vector3(distance, 0, 0);
            previousNodePosition = new Vector3(-distance, 0, 0);

            CurrentNode = graph.Entrance;
        }

        public void MoveTo(Node node)
        {
            PreviousNode = CurrentNode;
            CurrentNode = node;
            Position();
            ToggleVisibility();
        }

        public void Position()
        {
            CurrentNode.transform.localPosition = pivotPosition;

            var nodes = PartnerNodes;
            var nodesGroup = new NodePositionGroup(nodes);
            if (nodesGroup.HasCenterNode)
            {
                nodesGroup.CenterNode.transform.position = centerNodePosition;
            }

            RotateNodes(
                nodesGroup.NodesLeftSide, 
                NodePositionSide.Left, 
                nodesGroup.HasCenterNode);
            RotateNodes(
                nodesGroup.NodesRightSide, 
                NodePositionSide.Right, 
                nodesGroup.HasCenterNode);

            if (PreviousNode != null)
            {
                PreviousNode.transform.position = previousNodePosition;
            }

            foreach (var node in nodes)
            {
                Debug.Log("Setting visible for node: " + node.ToString());
                node.SetVisible(true);
            }
        }

        public void ToggleVisibility()
        {
            var visibleNodes = ActiveNodes;
            var hiddenNodes = graph.Nodes.Except(visibleNodes);
            Debug.Log("Visible: " + visibleNodes.Count() + " | Hidden: " + hiddenNodes.Count());
            foreach (var node in visibleNodes)
            {
                node.SetVisible(true);
            }
            foreach (var node in hiddenNodes)
            {
                node.SetVisible(false);
            }
        }

        void RotateNodes(
            IEnumerable<Node> nodes, 
            NodePositionSide side,
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
                var actualAngle = 
                    side == NodePositionSide.Left ? -remainingAngle : remainingAngle;
                var angles = new Vector3(0, actualAngle, 0);
                node.transform.position = 
                    centerNodePosition.RotatePointAroundPivot(Pivot, angles);

                remainingAngle += angleDiff;
            }
        }

        enum NodePositionSide
        {
            Right,
            Left
        }

        class NodePositionGroup
        {
            public readonly IEnumerable<Node> NodesLeftSide;
            public readonly IEnumerable<Node> NodesRightSide;
            public readonly Node CenterNode;

            public bool HasCenterNode
            { 
                get { return CenterNode != null; }
            }

            public NodePositionGroup(IEnumerable<Node> nodes)
            {
                var queue = nodes.ToQueue();
                if (nodes.Count().IsOdd())
                {
                    CenterNode = queue.Dequeue(); 
                }

                var leftNodes = new List<Node>();
                var rightNodes = new List<Node>();

                while (queue.Count > 0)
                {
                    leftNodes.Add(queue.Dequeue());
                    rightNodes.Add(queue.Dequeue());
                }

                NodesLeftSide = leftNodes;
                NodesRightSide = rightNodes;
            }
        }
    }
}

