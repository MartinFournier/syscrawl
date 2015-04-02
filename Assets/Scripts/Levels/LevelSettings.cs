using UnityEngine;

namespace syscrawl.Levels
{
    public class LevelSettings : MonoBehaviour
    {

        public int MinimumNodes = 20;
        public int MaximumNodes = 50;

        public int GetRandomNumberOfNodes()
        {
            return Random.Range(
                MinimumNodes, 
                MaximumNodes + 1);
        }

        public Vector3 NodeMinimumScale = new Vector3(1, 1, 1);
        public Vector3 NodeMaximumScale = new Vector3(10, 30, 30);

        public float MinimumNodeDistance = 8;
        public float MaximumNodeDistance = 20;
	
        public int NodeExtraEdges = 10;
        public int NodeExtraEdgesAttempts = 100;
    
    }
}