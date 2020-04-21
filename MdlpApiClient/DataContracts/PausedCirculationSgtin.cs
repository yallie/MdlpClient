namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.12.2. Получение перечня КИЗ по конкретному решению о приостановке
    /// </summary>
    [DataContract]
    public class PausedCirculationSgtin
    {
        /// <summary>
        /// SGTIN (КИЗ)
        /// </summary>
        [DataMember(Name = "sgtin", IsRequired = true)]
        public string Sgtin { get; set; }

        /// <summary>
        /// GTIN
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "gtin", IsRequired = false)]
        public string Gtin { get; set; }

        /// <summary>
        /// Номер производственной серии
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "batch", IsRequired = false)]
        public string BatchNumber { get; set; }

        /// <summary>
        /// Идентификатор текущего владельца
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "owner_id", IsRequired = false)]
        public string OwnerID { get; set; }

        /// <summary>
        /// Местонахождение — адрес
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "address", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// Местонахождение — код субъекта РФ
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "federal_subject_code", IsRequired = false)]
        public string FederalSubjectCode { get; set; }

        /// <summary>
        /// ИНН текущего владельца
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "owner_inn", IsRequired = false)]
        public string OwnerInn { get; set; }

        /// <summary>
        /// Наименование текущего владельца
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "owner_name", IsRequired = false)]
        public string OwnerName { get; set; }

        /// <summary>
        /// Международное непатентованное наименование, или группировочное, или химическое наименование
        /// Например: ТРАСТУЗУМАБ
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "prod_name", IsRequired = false)]
        public string ProductName { get; set; }

        /// <summary>
        /// Торговое наименованиe
        /// Например: Гертикад®
        /// </summary>
        /// <remarks>
        /// Поле бывает пусто, полагаться нельзя
        /// </remarks>
        [DataMember(Name = "product_sell_name", IsRequired = false)]
        public string ProductSellingName { get; set; }
    }
}
