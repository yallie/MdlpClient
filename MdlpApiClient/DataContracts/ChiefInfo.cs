namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// Формат объекта ChiefInfo
    /// </summary>
    [DataContract]
    public class ChiefInfo
    {
        /// <summary>
        /// Имя руководителя организации
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество руководителя организации
        /// </summary>
        [DataMember(Name = "middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия руководителя организации
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
    }
}
