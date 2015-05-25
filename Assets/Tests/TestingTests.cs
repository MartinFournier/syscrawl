using NUnit.Framework;

namespace syscrawl.Tests
{
    [TestFixture()]
    public class TestingTests
    {
        [Test()]
        public void Thing()
        {
            Assert.IsTrue(true);
        }

        [Test()]
        public void Err()
        {
            Assert.IsTrue(false);
        }
    }
}

