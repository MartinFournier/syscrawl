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

        #region Events

        public delegate void MovedToNodeEventHandler(Vector3 newCenterPosition);

        public event MovedToNodeEventHandler MovedToNode;

        #endregion

        #region Members & Properties

        readonly PositioningSettings settings;

        readonly NodesGraph graph;

        Node CurrentNode { get; set; }

        Node PreviousNode { get; set; }

        #endregion

        IEnumerable<Node> ActiveNodes
        {
            get
            { 
                var nodes = 
                    CurrentNode.GetConnections().ToList();
                
                nodes.Add(CurrentNode);

                return nodes;
            }
        }

        IEnumerable<Node> ActiveNodesAhead
        {
            get
            {
                var nodesAhead = new List<Node>();
                var currentNodeConnections = CurrentNode.GetConnections();

                foreach (var node in currentNodeConnections)
                {
                    var connectionsAhead = node.GetConnections(CurrentNode);
                    nodesAhead.AddRange(connectionsAhead);
                }
             
                return nodesAhead.Distinct();
            }
        }

        public Positioning(
            NodesGraph graph, 
            float angle, 
            float distance)
        {
            settings = new PositioningSettings(angle, distance);

            this.graph = graph;
            CurrentNode = graph.Entrance;
        }

        public void MoveTo(Node node)
        {
            settings.Pivot = node.Position;
            PreviousNode = CurrentNode;
            CurrentNode = node;
            Position();
            ToggleVisibility();

            if (MovedToNode != null)
            {
                MovedToNode(settings.Pivot);
            }
        }

        public void Position()
        {
            CurrentNode.Position = settings.Pivot;

            var nodes = CurrentNode.GetConnections(PreviousNode);
            var nodesGroup = new NodePositionGroup(nodes);

            PositionNodesGroup(
                nodesGroup, 
                settings.Pivot, 
                settings.angle, 
                settings.CenterNodePosition);

            if (PreviousNode != null)
            {
                PreviousNode.Position = settings.PreviousNodePosition;
            }

            foreach (var node in nodes)
            {
                // Will have issue here with nodes that are linked to multiple, 
                // TODO: Need to clone them
                var extraNodes = node.GetConnections(CurrentNode);
                var ng = new NodePositionGroup(extraNodes);

                var pivot = node.Position;
                var newPoint = pivot + new Vector3(10, 0, 0);

                Debug.Log("Positioning Node " + node);
                Debug.Log("From " + pivot + " towards " + newPoint);
                Debug.Log("NbNodes: " + ng.Count);

                PositionNodesGroup(ng, pivot, 90f, newPoint);

                foreach (var extraNode in extraNodes)
                {
                    extraNode.Scale = 
                        new Vector3(0.3f, 0.3f, 0.3f);
                }
            }

            foreach (var node in nodes)
            {
                node.Scale = new Vector3(1, 1, 1);
            }
        }

        public void ToggleVisibility()
        {
            var visibleNodes = ActiveNodes.Union(ActiveNodesAhead);
            var hiddenNodes = graph.Nodes.Except(visibleNodes);

            foreach (var node in visibleNodes)
            {
                node.SetVisible(true);
            }
            foreach (var node in hiddenNodes)
            {
                node.SetVisible(false);
            }
        }

        #region Position Settings

        class PositioningSettings
        {
            readonly Vector3 distance;

            public readonly float angle;

            public Vector3 Pivot { get; set; }

            public Vector3 CenterNodePosition
            { 
                get
                { 
                    return Pivot + distance;
                }
            }

            public Vector3 PreviousNodePosition
            { 
                get
                { 
                    return Pivot - distance;
                }
            }

            public PositioningSettings(
                float angle, 
                float distance)
            {
                this.angle = angle;
                this.distance = new Vector3(distance, 0, 0);

                Pivot = new Vector3(0, 0, 0);
            }
        }

        #endregion

        #region Position Rotation

        void PositionNodesGroup(
            NodePositionGroup nodesGroup,
            Vector3 pivot, 
            float maxAngle,
            Vector3 nodePosition)
        {
            
            if (nodesGroup.HasCenterNode)
            {
                nodesGroup.CenterNode.Position = nodePosition;
            }

            RotateNodesGroup(
                nodesGroup, 
                pivot, 
                maxAngle, 
                nodePosition);

        }

        void RotateNodesGroup(
            NodePositionGroup nodesGroup, 
            Vector3 pivot, 
            float maxAngle,
            Vector3 nodePosition
        )
        {
            RotateNodes(
                nodesGroup.NodesLeftSide, 
                NodePositionSide.Left, 
                nodesGroup.HasCenterNode,
                pivot, maxAngle, nodePosition);
            RotateNodes(
                nodesGroup.NodesRightSide, 
                NodePositionSide.Right, 
                nodesGroup.HasCenterNode,
                pivot, maxAngle, nodePosition);
        }

        void RotateNodes(
            IEnumerable<Node> nodes, 
            NodePositionSide side,
            bool hasCenter,
            Vector3 pivot,
            float maxAngle,
            Vector3 nodePosition
        )
        {
            var angleDiff = maxAngle / nodes.Count();
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
                var newPosition = 
                    nodePosition.RotatePointAroundPivot(pivot, angles);

                node.Position = newPosition;

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

            public int Count
            {
                get
                { 
                    var nb = NodesLeftSide.Count() + NodesRightSide.Count();
                    if (HasCenterNode)
                        nb++;
                    
                    return nb;
                }
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

        #endregion
    }
}

