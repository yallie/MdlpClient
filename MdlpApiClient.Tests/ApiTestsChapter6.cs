namespace MdlpApiClient.Tests
{
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;
    using MdlpApiClient.DataContracts;
    using NUnit.Framework;

    [TestFixture]
    public class ApiTestsChapter6 : UnitTestsBase
    {
        private MdlpClient Client = new MdlpClient(credentials: new ResidentCredentials
        {
            ClientID = ClientID1,
            ClientSecret = ClientSecret1,
            UserID = TestUserID,
        })
        {
            Tracer = TestContext.Progress.WriteLine
        };
    }
}
