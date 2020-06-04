namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.13.1. Получение сводной информации распределения ЛП
    /// Информация по статусам кодов маркировки в производственной серии
    /// </summary>
    [DataContract]
    public class BatchStatusInfo
    {
        /// <summary>
        /// Статус кодов маркировки в производственной серии, <see cref="BatchStatus"/>.
        /// </summary>
        [DataMember(Name = "status", IsRequired = true)]
        public string Status { get; set; }

        /// <summary>
        /// Наименование организации.
        /// </summary>
        [DataMember(Name = "organization_name", IsRequired = true)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Адрес МД/МОХ.
        /// </summary>
        [DataMember(Name = "address", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// Количество кодов маркировки в данном статусе.
        /// </summary>
        [DataMember(Name = "sgtin_amount", IsRequired = false)]
        public int SgtinAmount { get; set; }
    }
}
