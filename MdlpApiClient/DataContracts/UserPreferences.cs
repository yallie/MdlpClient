namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Настройки профиля текущего пользователя
    /// </summary>
    [DataContract]
    public class UserPreferences
    {
        /// <summary>
        /// Язык интерфейса пользователя (ru/en)
        /// </summary>
        [DataMember(Name = "language", IsRequired = true)]
        public string Language { get; set; }
    }
}
