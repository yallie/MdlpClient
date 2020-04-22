namespace MdlpApiClient.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTestsClientBase : UnitTestsBase
    {
        public UnitTestsClientBase()
        {
            Client = CreateClient();
        }

        protected MdlpClient Client { get; private set; }

        protected virtual MdlpClient CreateClient()
        {
            return new MdlpClient(credentials: new NonResidentCredentials
            {
                ClientID = ClientID1,
                ClientSecret = ClientSecret1,
                UserID = UserStarter1,
                Password = UserPassword1,
            })
            {
                Tracer = WriteLine
            };
        }

        public override void Dispose()
        {
            if (Client != null)
            {
                Client.Dispose();
                Client = null;
            }

            base.Dispose();
        }
    }
}
