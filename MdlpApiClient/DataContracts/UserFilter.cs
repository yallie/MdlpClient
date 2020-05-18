namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.7.2. Метод для поиска зарегистрированных пользователей по фильтру
    /// Формат объекта UserFilter
    /// </summary>
    [DataContract]
    public class UserFilter
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DataMember(Name = "first_name", IsRequired = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [DataMember(Name = "last_name", IsRequired = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [DataMember(Name = "middle_name", IsRequired = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        [DataMember(Name = "login", IsRequired = false)]
        public string Login { get; set; }

        /// <summary>
        /// Признак администратора участника
        /// </summary>
        [DataMember(Name = "is_admin", IsRequired = false)]
        public bool? IsAdmin { get; set; }

        /// <summary>
        /// Статусы пользователей, см. значения <see cref="UserStatus"/>.
        /// </summary>
        [DataMember(Name = "statuses", IsRequired = false)]
        public string[] Statuses { get; set; }
    }
}
