using NUnit.Framework;
using syscrawl.Common.Services;

namespace syscrawl.Tests
{
    [TestFixture]
    public class RandomGeneratorTests
    {
        [Test]
        public void TestSeed()
        {
            var rng1 = new RandomGenerator("SeedThing");
            var rng2 = new RandomGenerator("SeedThing");
            var value1 = rng1.Next();
            var value2 = rng2.Next();
            Assert.IsTrue(value1 == value2);
        }
    }
}

