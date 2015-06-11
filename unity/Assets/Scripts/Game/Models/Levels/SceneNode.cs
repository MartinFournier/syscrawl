using UnityEngine;

namespace syscrawl.Game.Models.Levels
{
    public class SceneNode
    {
        public readonly Vector3 position;
        public readonly SceneNodeType type;

        public SceneNode(
            SceneNodeType type, 
            Vector3 position)
        {
            this.type = type;
            this.position = position;
        }
    }
}
