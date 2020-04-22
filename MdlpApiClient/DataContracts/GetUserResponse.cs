namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 6.1.4. Метод для получения информации о пользователе
    /// </summary>
    [DataContract]
    internal class GetUserResponse
    {
        /// <summary>
        /// Свойства пользователя
        /// </summary>
        [DataMember(Name = "user", IsRequired = true)]
        public GroupedUser User { get; set; }
    }
}
