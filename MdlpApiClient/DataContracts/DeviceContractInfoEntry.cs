namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.38. Формат объекта DeviceContractInfoEntry
    /// Таблица 34. Формат объекта DeviceContractInfoEntry    /// </summary>
    [DataContract]
    public class DeviceContractInfoEntry
    {
        /// <summary>
        /// Номер договора
        /// </summary>
        [DataMember(Name = "doc_num", IsRequired = false)]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата договора
        /// </summary>
        [DataMember(Name = "doc_date", IsRequired = false)]
        public DateTime? DocumentDate { get; set; }
    }
}
