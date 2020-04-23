namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.3. Метод для создания группы прав пользователей
    /// </summary>
    [DataContract]
    internal class CreateRightsGroupResponse
    {
        /// <summary>
        /// Уникальный идентификатор группы прав пользователей
        /// </summary>
        [DataMember(Name = "group_id", IsRequired = false)]
        public string GroupID { get; set; }
    }
}
