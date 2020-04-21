namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 7.9.1. Метод для получения списка стран
    /// Структура данных CountryInfo
    /// </summary>
    [DataContract]
    public class CountryInfo
    {
        /// <summary>
        /// Уникальный идентификатор, почти всегда пуст
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public string ID { get; set; }

        /// <summary>
        /// Код ISO
        /// </summary>
        [DataMember(Name = "iso", IsRequired = true)]
        public string Iso { get; set; }

        /// <summary>
        /// Двухзначное обозначение
        /// </summary>
        [DataMember(Name = "alpha2", IsRequired = true)]
        public string Alpha2 { get; set; }

        /// <summary>
        /// Трехзначное обозначение
        /// </summary>
        [DataMember(Name = "alpha3", IsRequired = true)]
        public string Alpha3 { get; set; }

        /// <summary>
        /// Расположение
        /// </summary>
        [DataMember(Name = "location", IsRequired = true)]
        public string Location { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Полное наименование, бывает не заполнено
        /// </summary>
        [DataMember(Name = "fullname", IsRequired = false)]
        public string FullName { get; set; }

        /// <summary>
        /// Англоязычное наименование
        /// </summary>
        [DataMember(Name = "english", IsRequired = true)]
        public string EnglishName { get; set; }

        /// <summary>
        /// Точное расположение
        /// </summary>
        [DataMember(Name = "location-precise", IsRequired = true)]
        public string LocationPrecise { get; set; }
    }
}
