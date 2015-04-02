using UnityEngine;

namespace syscrawl.Levels.Graph
{
    public class NodePosition
    {
        const float scaleFactor = 6f;

        public Vector3 LogicalPosition { get; set; }

        public Vector3 PhysicalPosition
        {
            get
            { 
                return new Vector3(
                    LogicalPosition.x * scaleFactor, 
                    LogicalPosition.y * scaleFactor, 
                    LogicalPosition.z * scaleFactor);
            }
        }

        public NodePosition()
        {
        }

        public NodePosition(Vector3 logicalPosition)
        {
            LogicalPosition = logicalPosition;
        }

        public NodePosition(float x, float y, float z)
            : this(new Vector3(x, y, z))
        {
        }
    }
}

