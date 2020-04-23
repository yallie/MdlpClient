namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.4. Метод для получения информации о группе прав пользователей
    /// </summary>
    [DataContract]
    public class Group
    {
        /// <summary>
        /// Уникальный идентификатор группы прав пользователей
        /// </summary>
        [DataMember(Name = "group_id")]
        public string GroupID { get; set; }

        /// <summary>
        /// Название группы прав пользователей
        /// </summary>
        [DataMember(Name = "group_name")]
        public string GroupName { get; set; }

        /// <summary>
        /// Список прав, см. <see cref="RightsEnum"/>
        /// </summary>
        [DataMember(Name = "rights")]
        public string[] Rights { get; set; }
    }
}
