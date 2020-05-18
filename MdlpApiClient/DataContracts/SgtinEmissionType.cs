namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.44. Типы эмиссии
    /// Таблица 40. Типы эмиссии
    /// </summary>
    public class SgtinEmissionType
    {
        /// <summary>
        /// Собственное производство.
        /// </summary>
        public const int OWN_PRODUCTION = 1;

        /// <summary>
        /// Контрактное производство.
        /// </summary>
        public const int CONTRACT_PRODUCTION = 2;

        /// <summary>
        /// Иностранное производство.
        /// </summary>
        public const int FOREIGN_PRODUCTION = 3;

        /// <summary>
        /// Маркирован в зоне таможенного контроля.
        /// </summary>
        public const int CUSTOMS_LABELING = 4;
    }
}
