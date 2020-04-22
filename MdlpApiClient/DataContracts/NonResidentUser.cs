namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Информация для регистрации пользователя-нерезидента
    /// </summary>
    [DataContract]
    public class NonResidentUser : UserBase
    {
        /// <summary>
        /// Пароль
        /// </summary>
        [DataMember(Name = "password", IsRequired = true)]
        public string Password { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }
    }
}
