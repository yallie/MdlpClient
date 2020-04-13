namespace MdlpApiClient
{
    using DataContracts;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 7: registries.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 7.1.1. Получение данных записи ЕГРЮЛ
        /// </summary>
        /// <returns>Данные из реестра ЕГРЮЛ</returns>
        public EgrulRegistryResponse GetEgrulRegistryEntry()
        {
            return Get<EgrulRegistryResponse>("reestr/egrul");
        }

        /// <summary>
        /// 7.2.1. Получение данных записи ЕГРИП
        /// </summary>
        /// <returns>Данные из реестра ЕГРИП</returns>
        public EgripRegistryResponse GetEgripRegistryEntry()
        {
            return Get<EgripRegistryResponse>("reestr/egrip");
        }

        /// <summary>
        /// 7.3.1. Получение записи реестра РАФП (реестра аккредитованных филиалов и представительств)
        /// </summary>
        /// <returns>Данные из реестра РАФП</returns>
        public RafpRegistryResponse GetRafpRegistryEntry()
        {
            return Get<RafpRegistryResponse>("reestr/rafp");
        }
    }
}
