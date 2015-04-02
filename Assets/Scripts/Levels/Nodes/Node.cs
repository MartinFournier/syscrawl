using UnityEngine;
using syscrawl.Utils;
using syscrawl.Levels;

namespace syscrawl.Levels.Nodes
{
    public class Node : MonoBehaviour
    {

        public GameObject cube;

        public void Generate(LevelSettings settings)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(this.transform);

            var scale = 
                RandomUtils.RandomVectorBetweenRange(
                    settings.nodeMinimumScale, 
                    settings.nodeMaximumScale);

            cube.transform.localScale = scale;
        }
    }
}