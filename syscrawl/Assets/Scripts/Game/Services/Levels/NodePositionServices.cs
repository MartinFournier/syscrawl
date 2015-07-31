using System;
using syscrawl.Game.Models.Levels;

namespace syscrawl.Game.Services.Levels
{
    public interface INodePositionServices
    {
        NodePositions GetPositions(
            LevelGraph level, 
            Node currentNode, 
            Node previousNode);
    }

    public class NodePositionServices : INodePositionServices
    {
        public NodePositions GetPositions(
            LevelGraph level, 
            Node currentNode, 
            Node previousNode)
        {
            // TODO: Cache the positioning something here
            var positions = 
                new NodePositions(
                    level,
                    currentNode,
                    previousNode,
                    90f, 20f
                );

            return positions;
        }
    }
}

