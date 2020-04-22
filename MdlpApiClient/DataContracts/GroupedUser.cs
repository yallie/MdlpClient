namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.22. Формат объекта GroupedUser
    /// Таблица 18. Формат объекта GroupedUser
    /// 6.1.4. Метод для получения информации о пользователе
    /// </summary>
    [DataContract]
    public class GroupedUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [DataMember(Name = "user_id", IsRequired = true)]
        public string UserID { get; set; }

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
        /// Группы прав, в которых состоит пользователь (строковые имена, а не коды)
        /// </summary>
        [DataMember(Name = "groups", IsRequired = true)]
        public string[] GroupNames { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        [DataMember(Name = "position", IsRequired = false)]
        public string Position { get; set; }

        /// <summary>
        /// Признак администратора участника
        /// </summary>
        [DataMember(Name = "is_admin", IsRequired = true)]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        /// <remarks>
        /// Поле не описано в документации.
        /// </remarks>
        [DataMember(Name = "login", IsRequired = false)]
        public string Login { get; set; }
    }
}
