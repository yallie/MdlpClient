namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    [DataContract]
    internal class RegisterResidentUserResponse
    {
        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
    }
}
