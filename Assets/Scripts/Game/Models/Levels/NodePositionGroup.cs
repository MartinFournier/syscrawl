using System;
using System.Collections.Generic;
using System.Linq;
using syscrawl.Common.Extensions;

namespace syscrawl.Game.Models.Levels
{
    public class NodePositionGroup
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
}
