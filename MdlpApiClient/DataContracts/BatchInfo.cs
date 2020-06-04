namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.13.1. Получение сводной информации распределения ЛП
    /// Информация о производственных сериях
    /// </summary>
    [DataContract]
    public class BatchInfo
    {
        /// <summary>
        /// Номер производственной серии.
        /// </summary>
        [DataMember(Name = "batch")]
        public string Batch { get; set; }

        /// <summary>
        /// Дата регистрации производственной серии.
        /// </summary>
        [DataMember(Name = "registration_date")]
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Количество кодов маркировки в производственной серии.
        /// </summary>
        [DataMember(Name = "size")]
        public int Size { get; set; }

        /// <summary>
        /// Информация по статусам кодов маркировки в производственной серии, <see cref="BatchStatusInfo"/>.
        /// </summary>
        [DataMember(Name = "statuses")]
        public BatchStatusInfo[] Statuses { get; set; }
    }
}
