using System;

namespace syscrawl.Services
{
    /// <summary>
    /// Since Unity's RNG cannot be instanciated, this uses the .NET one.
    /// </summary>
    public class RandomGenerator
    {
        Random random;

        void initialize(int seed)
        {
            random = new Random(seed);
        }

        public RandomGenerator(string seed = null)
        {
            int intSeed;
            if (string.IsNullOrEmpty(seed))
            {
                intSeed = new Random().Next();
            }
            else
            {
                intSeed = seed.GetHashCode();
            }

            initialize(intSeed);
        }

        public int Next()
        {
            return random.Next();
        }

        public float NextFloat()
        {
            return (float)random.NextDouble();
        }

        public bool NextBool()
        {
            return Range(0, 2) == 0;
        }

        /// <summary>
        /// Minimum is inclusive, maximum is exclusive
        /// </summary>
        /// <param name="minimum">Minimum.</param>
        /// <param name="maximum">Maximum.</param>
        public int Range(int minimum, int maximum)
        {
            
            return random.Next(minimum, maximum);
        }

        public float Range(float minimum, float maximum)
        {
            var value = 
                minimum +
                random.NextDouble() *
                (maximum - minimum);

            return (float)value;
        }

        public UnityEngine.Vector3 Range(
            UnityEngine.Vector3 minimum, UnityEngine.Vector3 maximum)
        {
            var x = Range(minimum.x, maximum.x + 1);
            var y = Range(minimum.y, maximum.y + 1);
            var z = Range(minimum.z, maximum.z + 1);

            return new UnityEngine.Vector3(x, y, z);
        }
    }
}

