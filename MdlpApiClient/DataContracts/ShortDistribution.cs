namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.13.1. Получение сводной информации распределения ЛП
    /// </summary>
    [DataContract]
    public class ShortDistribution
    {
        /// <summary>
        /// Уникальный идентификатор GTIN лекарственного препарата.
        /// </summary>
        [DataMember(Name = "gtin")]
        public string Gtin { get; set; }

        /// <summary>
        /// Информация о производственных сериях, см. <see cref="BatchInfo"/>
        /// </summary>
        [DataMember(Name = "batches")]
        public BatchInfo[] Batches { get; set; }
    }
}
