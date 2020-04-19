namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Формат объекта ForeignCounterparty
    /// </summary>
    /// <remarks>
    /// Похож на <see cref="ForeignCounterpartyEntry"/>, но увы, не идентичен.
    /// </remarks>
    [DataContract]
    public class ForeignCounterparty
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Идентификатор контрагента как субъекта обращения в «ИС "Маркировка". МДЛП»
        /// </summary>
        [DataMember(Name = "system_subj_id")]
        public string SystemSubjectID { get; set; }

        /// <summary>
        /// ITIN организации контрагента
        /// </summary>
        [DataMember(Name = "counterparty_itin")]
        public string Itin { get; set; }

        /// <summary>
        /// Наименование субъекта обращения
        /// </summary>
        [DataMember(Name = "counterparty_name")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Адрес субъекта обращения
        /// </summary>
        [DataMember(Name = "counterparty_address")]
        public ForeignAddress Address { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        [DataMember(Name = "op_date")]
        public OperationDate OperationDate { get; set; }
    }
}
