namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.2. Фильтр для получения информации о КИЗ, вложенных в третичную упаковку
    /// </summary>
    [DataContract]
    public class SsccSgtinsFilter
    {
        /// <summary>
        /// Признак, отображающий, относится ли ЛП к списку 7ВЗН
        /// </summary>
        [DataMember(Name = "vzn_drug", IsRequired = false)]
        public bool? VznDrug { get; set; }

        /// <summary>
        /// Признак наличия в ЖНВЛП
        /// </summary>
        [DataMember(Name = "gnvlp", IsRequired = false)]
        public bool? Gnvlp { get; set; }
    }
}
