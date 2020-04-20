namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.38. Формат объекта LicenseEntry
    /// Таблица 34. Формат объекта LicenseEntry
    /// 7.6.1. Получение информации о лицензиях на производство
    /// 7.6.2. Получение информации о лицензиях на производство по фильтру
    /// </summary>
    [DataContract]
    public class LicenseEntry
    {
        /// <summary>
        /// Идентификатор записи в реестре
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public string ID { get; set; } // "59f6fa41762afe8ac12021c9",

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        [DataMember(Name = "inn", IsRequired = true)]
        public string Inn { get; set; } // "4025175206",

        /// <summary>
        /// Название организации, которой выдана лицензия
        /// </summary>
        [DataMember(Name = "org_name", IsRequired = true)]
        public string OrganizationName { get; set; } // "ООО \"Медицина\"",

        /// <summary>
        /// Номер лицензии
        /// </summary>
        [DataMember(Name = "l_num", IsRequired = true)]
        public string LicenseNumber { get; set; } // "00233-ЛС",

        /// <summary>
        /// Статус лицензии
        /// </summary>
        [DataMember(Name = "l_status", IsRequired = true)]
        public string LicenseStatus { get; set; } // "действует",

        /// <summary>
        /// Дата начала действия лицензии
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = true)]
        public DateTime StartDate { get; set; } // "2016-09-13T00:00:00.000Z"

        /// <summary>
        /// Дата окончания действия лицензии
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; } // "2016-09-13T00:00:00.000Z"

        /// <summary>
        /// Адрес действия лицензии
        /// </summary>
        [DataMember(Name = "address", IsRequired = true)]
        public AddressFias Address { get; set; } // aoguid, houseguid

        /// <summary>
        /// Перечень работ/услуг согласно лицензии
        /// </summary>
        [DataMember(Name = "work_list", IsRequired = true)]
        public string[] WorkList { get; set; } // ["производство и хранение ЛП"]

        /// <summary>
        /// Признак невалидности кода ФИАС
        /// </summary>
        [DataMember(Name = "invalid_fias_code")]
        public bool InvalidFiasCode { get; set; } // false

        /// <summary>
        /// Адрес СМЭВ (адрес в текстовом виде)
        /// </summary>
        [DataMember(Name = "objects", IsRequired = false)]
        public string AddressDescription { get; set; } // "г Москва, ул Щипок, д. 9/26 стр. 3"
    }
}
