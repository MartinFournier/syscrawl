using UnityEngine;

namespace syscrawl.Utils
{
    public static class RandomUtils
    {

        public static Vector3 RandomVectorBetweenRange(
            Vector3 minimum, Vector3 maximum)
        {
            var x = Random.Range(minimum.x, maximum.x + 1);
            var y = Random.Range(minimum.y, maximum.y + 1);
            var z = Random.Range(minimum.z, maximum.z + 1);

            return new Vector3(x, y, z);
        }

        public static bool RandomBool()
        {
            return Random.Range(0, 2) == 0;
        }

    }
}
