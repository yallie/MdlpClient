namespace MdlpApiClient.Tests
{
    using System.Runtime.Serialization;
    using MdlpApiClient.Serialization;
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;
    using RestSharp;

    [TestFixture]
    public class SerializationTests
    {
        // нет, такое мы использовать не будем:
        [DataContract]
        public class Base
        {
            [DataMember(Name = "sys_id")]
            public string SystemSubjectID { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }

        [DataContract]
        public class Native : Base
        {
            [DataMember(Name = "inn")]
            public string Inn { get; set; }
        }

        [DataContract]
        public class Foreign : Base
        {
            [DataMember(Name = "itin")]
            public string Itin { get; set; }
        }

        [DataContract]
        public class Response
        {
            [DataMember(Name = "items")]
            public Base[] Items { get; set; }
        }

        [Test]
        public void JsonTest()
        {
            var r = new Response
            {
                Items = new Base[]
                {
                    new Base { SystemSubjectID = "1", Name = "Base1" },
                    new Native { SystemSubjectID = "2", Name = "Native2", Inn = "3121" },
                    new Foreign { SystemSubjectID = "3", Name = "Foreign3", Itin = "1231" },
                }
            };

            var s = new ServiceStackSerializer();
            var json = s.Serialize(r);
            Assert.NotNull(json);
            TestContext.Progress.WriteLine(JsonFormatter.FormatJson(json));

            var obj = s.Deserialize<Response>(new RestResponse() { Content = json });
            Assert.NotNull(obj);
        }
    }
}
