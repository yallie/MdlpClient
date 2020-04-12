namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.2. Метод для регистрации пользователей (для резидентов страны)
    /// </summary>
    [DataContract]
    internal class RegisterResidentUserResponse
    {
        [DataMember(Name = "user_id")]
        public string UserID { get; set; }
    }
}
