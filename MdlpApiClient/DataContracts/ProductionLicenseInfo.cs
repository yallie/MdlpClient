namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.6.1. Получение информации о лицензиях на производство
    /// </summary>
    /// <remarks>
    /// Похоже по структуре на <see cref="LicenseEntry"/>.
    /// </remarks>
    [DataContract]
    public class ProductionLicenseInfo
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
        [DataMember(Name = "ORG_NAME", IsRequired = true)]
        public string OrganizationName { get; set; } // "ООО \"Медицина\"",

        /// <summary>
        /// Номер лицензии
        /// </summary>
        [DataMember(Name = "L_NUM", IsRequired = true)]
        public string LicenseNumber { get; set; } // "00233-ЛС",

        /// <summary>
        /// Статус лицензии
        /// </summary>
        [DataMember(Name = "L_STATUS", IsRequired = true)]
        public string LicenseStatus { get; set; } // "действует",

        /// <summary>
        /// Дата начала действия лицензии
        /// </summary>
        [DataMember(Name = "START_DATE", IsRequired = true)]
        public DateTime StartDate { get; set; } // "2016-09-13T00:00:00.000Z"

        /// <summary>
        /// Дата окончания действия лицензии
        /// </summary>
        [DataMember(Name = "END_DATE", IsRequired = false)]
        public DateTime? EndDate { get; set; } // "2016-09-13T00:00:00.000Z"

        /// <summary>
        /// Адрес действия лицензии
        /// </summary>
        [DataMember(Name = "ADDRESS", IsRequired = true)]
        public AddressFias Address { get; set; } // aoguid, houseguid

        /// <summary>
        /// Перечень работ/услуг согласно лицензии
        /// </summary>
        [DataMember(Name = "WORK_LIST", IsRequired = true)]
        public string[] WorkList { get; set; } // ["производство и хранение ЛП"]
    }
}
