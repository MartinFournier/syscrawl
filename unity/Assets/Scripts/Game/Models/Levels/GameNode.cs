using UnityEngine;

namespace syscrawl.Models.Levels
{
    public class GameNode
    {
        public readonly Vector3 position;
        public readonly GameNodeType type;

        public GameNode(
            GameNodeType type, 
            Vector3 position)
        {
            this.type = type;
            this.position = position;
        }
    }
}
