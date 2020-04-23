namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.11. Метод для поиска списка групп прав пользователей по фильтру
    /// </summary>
    [DataContract]
    public class GroupFilter
    {
        /// <summary>
        /// Название группы прав пользователей
        /// </summary>
        [DataMember(Name = "group_name", IsRequired = false)]
        public string GroupName { get; set; }

        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [DataMember(Name = "user_id", IsRequired = false)]
        public string UserID { get; set; }

        /// <summary>
        /// Список прав пользователей, см. <see cref="RightsEnum"/>
        /// </summary>
        [DataMember(Name = "rights", IsRequired = false)]
        public string[] Rights { get; set; }

        /// <summary>
        /// Тип группы прав
        /// </summary>
        [DataMember(Name = "type", IsRequired = false)]
        public int Type { get; set; }
    }
}
