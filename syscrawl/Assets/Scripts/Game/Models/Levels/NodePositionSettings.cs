using UnityEngine;

namespace syscrawl.Game.Models.Levels
{
    public class NodePositionSettings
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

        public NodePositionSettings(
            float angle,
            float distance)
        {
            this.angle = angle;
            this.distance = new Vector3(distance, 0, 0);

            Pivot = new Vector3(0, 0, 0);
        }
    }
}

