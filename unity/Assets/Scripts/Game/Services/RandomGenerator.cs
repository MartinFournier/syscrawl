using System;

namespace syscrawl
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


    }
}

