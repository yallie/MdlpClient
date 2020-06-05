namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.24. Формат объекта GroupedUser
    /// Таблица 20. Формат объекта GroupedUser
    /// 6.1.4. Метод для получения информации о пользователе
    /// </summary>
    [DataContract]
    public class GroupedUser : UserBase
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [DataMember(Name = "user_id", IsRequired = true)]
        public string UserID { get; set; }

        /// <summary>
        /// Группы прав, в которых состоит пользователь (строковые имена, а не коды)
        /// </summary>
        [DataMember(Name = "groups", IsRequired = true)]
        public string[] GroupNames { get; set; }

        /// <summary>
        /// Признак администратора участника
        /// </summary>
        [DataMember(Name = "is_admin", IsRequired = true)]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <remarks>
        /// Поле не описано в документации.
        /// </remarks>
        [DataMember(Name = "login", IsRequired = false)]
        public string Login { get; set; }
    }
}
