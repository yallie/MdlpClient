namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.3. Получение информации о конкретном месте ответственного хранения
    /// </summary>
    [DataContract]
    public class GetWarehouseResponse
    {
        /// <summary>
        /// Уникальный идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "warehouse_id", IsRequired = false)]
        public string WarehouseID { get; set; }

        /// <summary>
        /// Адрес места ответственного хранения
        /// </summary>
        [DataMember(Name = "address", IsRequired = false)]
        public Address Address { get; set; }
    }
}
