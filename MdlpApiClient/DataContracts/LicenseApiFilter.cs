namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.39. Формат объекта LicenseApiFilter
    // Таблица 35. Формат объекта LicenseApiFilter
    /// 7.6.2. Метод фильтрации лицензий на производство
    /// </summary>
    [DataContract]
    public class LicenseApiFilter
    {
        /// <summary>
        /// Номер лицензии
        /// </summary>
        [DataMember(Name = "l_num", IsRequired = false)]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Статус лицензии
        /// </summary>
        [DataMember(Name = "l_status", IsRequired = false)]
        public string LicenseStatus { get; set; }

        /// <summary>
        /// Дата начала действия лицензии: начало периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date_from", IsRequired = false)]
        public DateTime? StartDateFrom { get; set; }

        /// <summary>
        /// Дата начала действия лицензии: окончание периода фильтрации
        /// </summary>
        [DataMember(Name = "start_date_to", IsRequired = false)]
        public DateTime? StartDateTo { get; set; }
    }
}
