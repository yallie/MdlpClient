namespace MdlpApiClient.Tests
{
    using NUnit.Framework;
    using RestSharp;
    using System;

    [TestFixture]
    public class MdlpClientTracingTests : UnitTestsBase
    {
        private string CR = Environment.NewLine;

        [Test]
        public void FormatHeadersTest()
        {
            Assert.AreEqual("headers: none" + CR, MdlpClient.FormatHeaders(null));
            Assert.AreEqual("headers: none" + CR, MdlpClient.FormatHeaders(new Tuple<string, object>[0]));
            Assert.AreEqual(@"headers: {
  Content-type = text/plain
}
", MdlpClient.FormatHeaders(new[] { Tuple.Create("Content-type", "text/plain" as object) }));
        }

        [Test]
        public void FormatBodyTest()
        {
            Assert.AreEqual(string.Empty, MdlpClient.FormatBody(null));
            Assert.AreEqual(string.Empty, MdlpClient.FormatBody(new RequestBody(string.Empty, string.Empty, null)));
            Assert.AreEqual("body: Hello!" + CR, MdlpClient.FormatBody(new RequestBody("Text/plain", string.Empty, "Hello!")));
            Assert.AreEqual(@"body: {
  ""some"": 1
}
", MdlpClient.FormatBody(new RequestBody("application/json", string.Empty, "{\"some\": 1}")));
        }
    }
}
