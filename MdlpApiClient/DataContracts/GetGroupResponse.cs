namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.4. Метод для получения информации о группе прав пользователей
    /// </summary>
    [DataContract]
    internal class GetGroupResponse
    {
        /// <summary>
        /// Объект типа <see cref="Group"/>
        /// </summary>
        [DataMember(Name = "group", IsRequired = true)]
        public Group Group { get; set; }
    }
}
