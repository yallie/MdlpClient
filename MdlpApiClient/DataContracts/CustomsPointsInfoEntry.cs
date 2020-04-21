namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.11.1. Фильтрация по реестру мест таможенного контроля
    /// Формат объекта CustomPointsInfoEntry
    /// </summary>
    [DataContract]
    public class CustomsPointsInfoEntry
    {
        /// <summary>
        /// Уникальный идентификатор места нахождения товара в ЗТК
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public string ID { get; set; }

        /// <summary>
        /// ИНН владельца СВХ/ТС или УЭО        /// </summary>
        [DataMember(Name = "inn", IsRequired = false)]
        public string Inn { get; set; }

        /// <summary>
        /// Наименовани владельца СВХ/ТС или УЭО        /// </summary>
        [DataMember(Name = "orgName", IsRequired = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Номер свидетельства о включении в реестр ФТС России        /// </summary>
        [DataMember(Name = "regNum", IsRequired = false)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Тип склада        /// </summary>
        [DataMember(Name = "warehouseAddress", IsRequired = false)]
        public string WarehouseAddress { get; set; }

        /// <summary>
        /// Тип склада        /// </summary>
        [DataMember(Name = "warehouseType", IsRequired = false)]
        public string WarehouseType { get; set; }

        /// <summary>
        /// Код таможенного органа
        /// </summary>
        [DataMember(Name = "customCode", IsRequired = false)]
        public string CustomsCode { get; set; }

        /// <summary>
        /// Наименование таможенного органа
        /// </summary>
        [DataMember(Name = "customName", IsRequired = false)]
        public string CustomsName { get; set; }
    }
}
