namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.10.2. Фильтрация по реестру регистраторов выбытия
    /// </summary>
    [DataContract]
    public class WithdrawalDeviceInfoEntry
    {
        /// <summary>
        /// Уникальный идентификатор устройства
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = true)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Серийный (индивидуальный) номер устройства
        /// </summary>
        [DataMember(Name = "serial_number", IsRequired = true)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// Наименование (модель) устройства
        /// </summary>
        [DataMember(Name = "device_name", IsRequired = true)]
        public string DeviceName { get; set; }

        /// <summary>
        /// Идентификатор места деятельности согласно лицензии
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Дата предоставления
        /// </summary>
        [DataMember(Name = "provision_date", IsRequired = true)]
        public DateTime ProvisionDate { get; set; }

        /// <summary>
        /// Информация о договоре
        /// </summary>
        /// <remarks>
        /// Отсуствует в случае размещения в ЦОД оператора
        /// </remarks>
        [DataMember(Name = "contract_info", IsRequired = false)]
        public DeviceContractInfoEntry ContractInfo { get; set; }

        /// <summary>
        /// Тип размещения
        /// 0 — по месту использования
        /// 1 — ЦОД оператора
        /// 2 — по адресу МД
        /// </summary>
        [DataMember(Name = "placement_type", IsRequired = true)]
        public int PlacementType { get; set; }

        /// <summary>
        /// Статус
        /// 0 — активный
        /// 1 — неактивный
        /// </summary>
        [DataMember(Name = "status", IsRequired = true)]
        public int Status { get; set; }

        /// <summary>
        /// Производитель устройства
        /// </summary>
        [DataMember(Name = "device_vendor", IsRequired = true)]
        public string DeviceVendor { get; set; }

        /// <summary>
        /// Модель устройства
        /// </summary>
        [DataMember(Name = "device_model", IsRequired = true)]
        public string DeviceModel { get; set; }
    }
}
