namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.35. Формат объекта SgtinPaymentInformation
    /// </summary>
    [DataContract]
    public class SgtinPaymentInformation
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        [DataMember(Name = "created_date", IsRequired = false)]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Дата оплаты платежа
        /// </summary>
        [DataMember(Name = "payment_date", IsRequired = false)]
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Тариф оплаты
        /// </summary>
        [DataMember(Name = "tariff", IsRequired = false)]
        public decimal? Tariff { get; set; }
    }
}
