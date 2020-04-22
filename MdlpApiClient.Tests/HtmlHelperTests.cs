namespace MdlpApiClient.Tests
{
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;

    [TestFixture]
    public class HtmlHelperTests : UnitTestsBase
    {
        [Test]
        public void TestExtractText()
        {
            var html = "<html><head><title>404 NotFound</title></head><body bgcolor=\"white\"><center><h1>404 Not Found</h1></center><hr><center>nginx/1.14.0</center></body></html>";
            var text = HtmlHelper.ExtractText(html);
            Assert.NotNull(text);
            Assert.AreEqual("404 Not Found\r\nnginx/1.14.0", text);
            WriteLine(text);
        }
    }
}
