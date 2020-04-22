namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.1. Метод для регистрации учетной системы
    /// </summary>
    [DataContract]
    public class AccountingSystem
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [DataMember(Name = "client_id", IsRequired = true)]
        public string ClientID { get; set; }

        /// <summary>
        /// Секретный ключ
        /// </summary>
        [DataMember(Name = "client_secret", IsRequired = true)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Уникальный идентификатор  учетной системы
        /// </summary>
        [DataMember(Name = "account_system_id", IsRequired = true)]
        public string AccountingSystemID { get; set; }
    }
}
