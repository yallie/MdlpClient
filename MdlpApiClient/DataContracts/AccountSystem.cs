namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.1. Метод для регистрации учетной системы
    /// 6.1.11. Метод для получения информации об УС
    /// </summary>
    [DataContract]
    public class AccountSystem
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        [DataMember(Name = "client_id", IsRequired = true)]
        public string ClientID { get; set; }

        /// <summary>
        /// Секретный ключ клиента (возвращается только при регистрации УС)
        /// </summary>
        [DataMember(Name = "client_secret", IsRequired = false)]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Уникальный идентификатор  учетной системы
        /// </summary>
        [DataMember(Name = "account_system_id", IsRequired = true)]
        public string AccountSystemID { get; set; }

        /// <summary>
        /// Название УС (возвращается только при запросе параметров УС)
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public string Name { get; set; }
    }
}
