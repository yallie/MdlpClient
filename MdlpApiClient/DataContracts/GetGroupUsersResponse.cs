namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.6.5. Метод для получения информации о пользователях группы
    /// </summary>
    [DataContract]
    internal class GetGroupUsersResponse
    {
        /// <summary>
        /// Объекты типа <see cref="User"/>
        /// </summary>
        [DataMember(Name = "users", IsRequired = true)]
        public User[] Users { get; set; }
    }
}
