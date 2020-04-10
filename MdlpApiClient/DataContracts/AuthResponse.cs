namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class AuthResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
