namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.4. Метод для регистрация места ответственного хранения
    /// </summary>
    [DataContract]
    internal class RegisterWarehouseResponse
    {
        [DataMember(Name = "safe_warehouse_id")]
        public string WarehouseID { get; set; }
    }
}
