namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.36. Формат объекта SgtinBillingInformation
    /// Таблица 32. Формат объекта SgtinBillingInformation
    /// </summary>
    [DataContract]
    public class SgtinBillingInformation
    {
        /// <summary>
        /// Признак предоплаты
        /// </summary>
        [DataMember(Name = "is_prepaid")]
        public bool IsPrepaid { get; set; }

        /// <summary>
        /// Признак бесплатного кода
        /// </summary>
        [DataMember(Name = "free_code")]
        public bool FreeCode { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        [DataMember(Name = "is_paid")]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Признак вхождения в список высокозатратных нозологий        /// </summary>
        [DataMember(Name = "contains_vzn")]
        public bool ContainsVzn { get; set; }

        /// <summary>
        /// Список информации о платежах        /// </summary>
        [DataMember(Name = "payments")]
        public SgtinPaymentInformation[] Payments { get; set; }
    }
}
