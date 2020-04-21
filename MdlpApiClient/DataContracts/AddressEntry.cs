namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.8.1. Метод для получения информации о всех местах осуществления
    /// деятельности и местах ответственного хранения участника
    /// </summary>
    [DataContract]
    public class AddressEntry
    {
        /// <summary>
        /// Идентификатор места осуществления деятельности или идентификатор места ответственного хранения
        /// </summary>
        [DataMember(Name = "address_id")]
        public string AddressID { get; set; }

        /// <summary>
        /// Адрес места осуществления деятельности или места ответственного хранения
        /// </summary>
        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// Тип записи
        /// 0 — место осуществления деятельности
        /// 1 — место ответственного хранения
        /// </summary>
        [DataMember(Name = "entity_type")]
        public int EntityType { get; set; }
    }
}
