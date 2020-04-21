namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// </summary>
    [DataContract]
    internal class MemberResponse
    {
        /// <summary>
        /// Информация об организации, в которой зарегистрирован текущий пользователь
        /// </summary>
        [DataMember(Name = "member", IsRequired = false)]
        public Member Member { get; set; }
    }
}
