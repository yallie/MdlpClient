namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.10.2. Фильтрация по реестру регистраторов выбытия
    /// </summary>
    [DataContract]
    public class WithdrawalDeviceFilter
    {
        /// <summary>
        /// Уникальный идентификатор устройства
        /// </summary>
        [DataMember(Name = "device_id", IsRequired = false)]
        public string DeviceID { get; set; }

        /// <summary>
        /// Дата предоставления, начало периода фильтрации
        /// </summary>
        [DataMember(Name = "provision_start_date", IsRequired = false)]
        public DateTime? ProvisionStartDate { get; set; }

        /// <summary>
        /// Дата предоставления, окончание периода фильтрации
        /// </summary>
        [DataMember(Name = "provision_end_date", IsRequired = false)]
        public DateTime? ProvisionEndDate { get; set; }

        /// <summary>
        /// Идентификатор места деятельности согласно лицензии
        /// </summary>
        [DataMember(Name = "branch_id", IsRequired = false)]
        public string BranchID { get; set; }

        /// <summary>
        /// Тип размещения
        /// 0 — по месту использования
        /// 1 — ЦОД оператора
        /// 2 — по адресу МД
        /// </summary>
        [DataMember(Name = "placement_type", IsRequired = false)]
        public int? PlacementType { get; set; }

        /// <summary>
        /// Статус
        /// 0 — активный
        /// 1 — неактивный
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Производитель устройства
        /// </summary>
        [DataMember(Name = "device_vendor", IsRequired = false)]
        public string DeviceVendor { get; set; }

        /// <summary>
        /// Модель устройства
        /// </summary>
        [DataMember(Name = "device_model", IsRequired = false)]
        public string DeviceModel { get; set; }
    }
}
