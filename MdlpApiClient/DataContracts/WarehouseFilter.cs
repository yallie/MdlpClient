namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.2. Метод для поиска информации о местах ответственного хранения по фильтру
    /// </summary>
    [DataContract]
    public class WarehouseFilter
    {
        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "warehouse_id", IsRequired = false)]
        public string WarehouseID { get; set; }

        /// <summary>
        /// Уникальный идентификатор дома
        /// </summary>
        [DataMember(Name = "houseguid", IsRequired = false)]
        public string HouseGuid { get; set; }

        /// <summary>
        /// Статус: 0 — не действует, 1 — действует, 2 — в процессе приостановления
        /// </summary>
        [DataMember(Name = "status", IsRequired = false)]
        public int? Status { get; set; }

        /// <summary>
        /// Дата начала периода фильтрации по дате регистрации
        /// </summary>
        [DataMember(Name = "start_date", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода фильтрации по дате регистрации
        /// </summary>
        [DataMember(Name = "end_date", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }
}
