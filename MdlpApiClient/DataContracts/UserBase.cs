namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Базовая информация о пользователе
    /// </summary>
    [DataContract]
    public abstract class UserBase
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DataMember(Name = "first_name", IsRequired = true)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DataMember(Name = "last_name", IsRequired = true)]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [DataMember(Name = "middle_name", IsRequired = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        [DataMember(Name = "position", IsRequired = false)]
        public string Position { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { get; set; }
    }
}
