namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.30. Формат объекта UserEditProfileEntry
    /// Таблица 26. Формат объекта UserEditProfileEntry
    /// </summary>
    [DataContract]
    public class UserEditProfileEntry : UserBase    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }
    }
}
