namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.6.2. Метод для просмотра заявок на регистрацию иностранных контрагентов
    /// Формат объекта ForeignCounterpartyEntry
    /// </summary>
    [DataContract]
    public class ForeignCounterpartyEntry
    {
        /// <summary>
        /// Код страны
        /// </summary>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Идентификатор контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "system_subj_id", IsRequired = false)]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// Время подачи заявки
        /// </summary>
        [DataMember(Name = "op_date")]
        public DateTime OperationDate { get; set; }

        /// <summary>
        /// Описание результата операции
        /// </summary>
        [DataMember(Name = "detailed_code", IsRequired = false)]
        public int? DetailedCode { get; set; }

        /// <summary>
        /// Результат операции
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// ИНН/ITIN организации контрагента
        /// </summary>
        [DataMember(Name = "inn")]
        public string Inn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Время выполнения заявки
        /// </summary>
        [DataMember(Name = "op_exec_date")]
        public DateTime? OperationExecutionDate { get; set; }
    }
}
