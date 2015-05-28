using NUnit.Framework;

namespace syscrawl.Tests
{
    [TestFixture]
    public class RandomGeneratorTests
    {
        [Test]
        public void TestSeed()
        {
            var rng1 = new Services.RandomGenerator("SeedThing");
            var rng2 = new Services.RandomGenerator("SeedThing");
            var value1 = rng1.Next();
            var value2 = rng2.Next();
            Assert.IsTrue(value1 == value2);
        }
    }
}

