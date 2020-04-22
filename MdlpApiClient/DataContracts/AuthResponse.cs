namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.2.1. Метод для получения кода аутентификации
    /// </summary>
    [DataContract]
    internal class AuthResponse
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
    }
}
