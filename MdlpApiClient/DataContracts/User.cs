namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.23. Формат объекта User
    /// Таблица 19. Формат объекта User
    /// </summary>
    [DataContract]
    public class User : UserBase
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        [DataMember(Name = "user_id", IsRequired = true)]
        public string UserID { get; set; }
    }
}
