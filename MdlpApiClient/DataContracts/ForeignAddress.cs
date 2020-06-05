namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 4.29. Формат объекта ForeignAddress
    /// Таблица 25. Формат объекта ForeignAddress
    /// 8.6.1. Метод для регистрации иностранного контрагента
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// </summary>
    [DataContract]
    public class ForeignAddress
    {
        /// <summary>
        /// Город
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        [DataMember(Name = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Населённый пункт
        /// </summary>
        [DataMember(Name = "locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        [DataMember(Name = "street")]
        public string Street { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [DataMember(Name = "house")]
        public string House { get; set; }

        /// <summary>
        /// Корпус
        /// </summary>
        [DataMember(Name = "corpus")]
        public string Corpus { get; set; }

        /// <summary>
        /// Литера
        /// </summary>
        [DataMember(Name = "litera")]
        public string Litera { get; set; }

        /// <summary>
        /// № помещения (квартиры)        /// </summary>
        [DataMember(Name = "room")]
        public string Room { get; set; }
    }
}
