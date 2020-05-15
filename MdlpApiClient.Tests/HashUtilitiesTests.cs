namespace MdlpApiClient.Tests
{
    using System.Security.Cryptography;
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;

    [TestFixture]
    public class HashUtilitiesTests : UnitTestsBase
    {
        [Test]
        public void ToHexStringTests()
        {
            Assert.AreEqual(string.Empty, HashUtilities.ToHexString(null));
            Assert.AreEqual(string.Empty, HashUtilities.ToHexString(new byte[0]));
            Assert.AreEqual("cafebabe", HashUtilities.ToHexString(new byte[] { 0xca, 0xfe, 0xba, 0xbe }));
        }

        [Test]
        public void ComputeHashTests()
        {
            Assert.AreEqual("a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3", "123".ComputeHash<SHA256>());
            Assert.AreEqual("3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2", "123".ComputeHash<SHA512>());
        }

        [Test]
        public void IsNullOrEmptyWorksAsDesigned()
        {
            var items = new[] { "Hello", "World!" };
            Assert.IsFalse(items.IsNullOrEmpty());
            Assert.IsTrue(new string[0].IsNullOrEmpty());

            items = null;
            Assert.IsTrue(items.IsNullOrEmpty());
        }

        [Test]
        public void EmptyIfNullWorksAsDesigned()
        {
            var items = new[] { "Hello", "World!" };
            Assert.AreSame(items, items.EmptyIfNull());

            items = null;
            Assert.IsNotNull(items.EmptyIfNull());
        }
    }
}
