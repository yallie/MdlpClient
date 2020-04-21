namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.2. Метод для изменения данных организации, в которой зарегистрирован текущий
    /// Для изменения доступна часть полей объекта <see cref="Member"/>.
    /// </summary>
    [DataContract]
    public class MemberOptions
    {
        /// <summary>
        /// Код языка квитанций
        /// </summary>
        /// <remarks>
        /// Ошибка в документации: похоже, это обязательный параметр.
        /// Без указания этого параметра метод 8.9.2 выполняется с ошибкой BadRequest (400).
        /// </remarks>
        [DataMember(Name = "language", IsRequired = true)]
        public string Language { get; set; }

        /// <summary>
        /// Код субъекта РФ (код места юридической регистрации участника)        /// </summary>
        [DataMember(Name = "registration_federal_subject_code", IsRequired = false)]
        public string RegistrationFederalSubjectCode { get; set; }

        /// <summary>
        /// Номер контактного телефона
        /// </summary>
        [DataMember(Name = "phone", IsRequired = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }

        /// <summary>
        /// Информация о банковских реквизитах участника
        /// </summary>
        [DataMember(Name = "banking_info", IsRequired = false)]
        public BankingInfo BankingInfo { get; set; }
    }
}
