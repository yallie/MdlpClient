namespace MdlpApiClient
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class MdlpAuthResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
