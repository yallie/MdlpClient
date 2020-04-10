using System.Runtime.Serialization;

namespace MdlpApiClient.DataContracts
{
    [DataContract]
    internal class RegisterResidentUserResponse
    {
        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
    }
}
