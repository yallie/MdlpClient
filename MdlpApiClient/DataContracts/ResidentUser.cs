namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Информация для регистрации пользователя-резидента
    /// </summary>
    [DataContract]
    public class ResidentUser : UserBase
    {
        /// <summary>
        /// Публичный сертификат пользователя в кодировке BASE64
        /// </summary>
        /// <remarks>
        /// Необходимо использовать публичный сертификат, а не публичный ключ
        /// </remarks>
        [DataMember(Name = "public_cert", IsRequired = true)]
        public string PublicCertificate { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }
    }
}
