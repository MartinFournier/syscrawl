using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Models.Levels
{
    public class NodePositions : Dictionary<Node, SceneNode>
    {
        readonly NodePositionSettings settings;
        readonly LevelGraph graph;
        readonly Node currentNode;
        readonly Node previousNode;

        public NodePositions(
            LevelGraph graph,
            Node currentNode,
            Node previousNode,
            float angle,
            float distance)
        {
            settings = new NodePositionSettings(angle, distance);

            this.graph = graph;
            this.currentNode = currentNode;
            this.previousNode = previousNode;

            Position();
        }

        void Position()
        {
            Add(
                currentNode, 
                new SceneNode(
                    SceneNodeType.Current, 
                    settings.Pivot)
            );

            var nodes = currentNode.GetConnections(previousNode);
            var nodesGroup = new NodePositionGroup(nodes);

            PositionNodesGroup(
                nodesGroup,
                settings.Pivot,
                settings.angle,
                settings.CenterNodePosition,
                SceneNodeType.Active
            );

            if (previousNode != null)
            {
                Add(
                    previousNode, 
                    new SceneNode(
                        SceneNodeType.Previous, 
                        settings.PreviousNodePosition)
                );
            }

            foreach (var node in nodes)
            {
                var extraNodes = node.GetConnections(currentNode);
                var ng = new NodePositionGroup(extraNodes);
                var pivot = this[node].position;
                //TODO: The pivot + settings.CenterNodePos needs to be changed
                var newPoint = 
                    pivot + new Vector3(
                        settings.CenterNodePosition.x / 3, 0, 0
                    );
                //TODO: Need to rotate second group to everything is a bit skewed

                PositionNodesGroup(
                    ng, pivot, 90f, newPoint, SceneNodeType.FurtherAhead);
            }
        }

        IEnumerable<Node> GetActiveNodes()
        {
            var nodes =
                currentNode.GetConnections().ToList();

            nodes.Add(currentNode);

            return nodes;
        }

        IEnumerable<Node> GetActiveNodesAhead()
        {

            var nodesAhead = new List<Node>();
            var currentNodeConnections = currentNode.GetConnections();

            foreach (var node in currentNodeConnections)
            {
                var connectionsAhead = node.GetConnections(currentNode);
                nodesAhead.AddRange(connectionsAhead);
            }

            return nodesAhead.Distinct();

        }

        void PositionNodesGroup(
            NodePositionGroup nodesGroup,
            Vector3 pivot,
            float maxAngle,
            Vector3 nodePosition,
            SceneNodeType type)
        {

            if (nodesGroup.HasCenterNode)
            {
                Add(
                    nodesGroup.CenterNode, 
                    new SceneNode(type, nodePosition)
                );
            }

            RotateNodesGroup(
                nodesGroup,
                pivot,
                maxAngle,
                nodePosition,
                type);
        }

        void RotateNodesGroup(
            NodePositionGroup nodesGroup,
            Vector3 pivot,
            float maxAngle,
            Vector3 nodePosition,
            SceneNodeType type
        )
        {
            RotateNodes(
                nodesGroup.NodesLeftSide,
                NodePositionSide.Left,
                nodesGroup.HasCenterNode,
                pivot, maxAngle, nodePosition, type);
            RotateNodes(
                nodesGroup.NodesRightSide,
                NodePositionSide.Right,
                nodesGroup.HasCenterNode,
                pivot, maxAngle, nodePosition, type);
        }

        void RotateNodes(
            IEnumerable<Node> nodes,
            NodePositionSide side,
            bool hasCenter,
            Vector3 pivot,
            float maxAngle,
            Vector3 nodePosition,
            SceneNodeType type
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

                Add(
                    node,
                    new SceneNode(type, newPosition)
                );

                remainingAngle += angleDiff;
            }
        }

    }
}

