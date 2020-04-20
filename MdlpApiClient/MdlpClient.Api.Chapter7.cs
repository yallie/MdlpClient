namespace MdlpApiClient
{
    using System;
    using System.Collections.Generic;
    using DataContracts;
    using RestSharp;

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

        /// <summary>
        /// 7.5.1. Получение объекта ФИАС по идентификатору адресного объекта
        /// </summary>
        /// <param name="addressId">Идентификатор адресного объекта</param>
        /// <returns>Данные из реестра ФИАС</returns>
        public FiasAddressObject GetFiasAddressObject(string addressId)
        {
            return Get<FiasAddressObject>("reestr/fias/addrobj/{addrobj}", new[]
            {
                new Parameter("addrobj", addressId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 7.5.2. Получение объекта ФИАС по идентификатору дома
        /// </summary>
        /// <param name="addressId">Идентификатор дома</param>
        /// <returns>Данные из реестра ФИАС</returns>
        public FiasHouseObject GetFiasHouseObject(string houseGuid)
        {
            return Get<FiasHouseObject>("reestr/fias/house/{houseobj}", new[]
            {
                new Parameter("houseobj", houseGuid, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 7.5.3. Получение текстового адреса по идентификаторам ФИАС
        /// </summary>
        /// <param name="houseGuid">Идентификатор дома</param>
        /// <param name="aoGuid">Идентификатор адреса</param>
        /// <param name="room">Комната (необязательно)</param>
        /// <returns>Данные из реестра ФИАС</returns>
        public AddressResolved GetFiasAddress(string houseGuid, string aoGuid = null, string room = null)
        {
            var address = Post<AddressResolved>("reestr/fias/resolve", new
            {
                // похоже, параметр aoGuid игнорируется, если указан дом, но наличие его все равно требуется
                aoguid = aoGuid ?? Guid.Empty.ToString(),
                houseguid = houseGuid,
                room = room,
            });

            // в ответе нет houseGuid, добавим для верности
            address.HouseGuid = houseGuid;
            return address;
        }

        /// <summary>
        /// 7.6.1. Получение информации о лицензиях на производство
        /// </summary>
        /// <returns>Список лицензий</returns>
        public ProductionLicenseInfo[] GetProductionLicenses()
        {
            return Get<List<ProductionLicenseInfo>>("reestr/prod_licenses").ToArray();
        }

        /// <summary>
        /// 7.6.2. Метод фильтрации лицензий на производство
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру лицензий на производство</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых лицензий на производство</param>
        /// <param name="count">Количество записей в списке возвращаемых лицензий на производство</param>
        /// <returns>Список лицензий</returns>
        public EntriesResponse<LicenseEntry> GetProductionLicenses(LicenseApiFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<LicenseEntry>>("reestr/prod_licenses", new
            {
                filter = filter ?? new LicenseApiFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 7.6.3. Метод для актуализации данных текущего участника из реестра лицензий на производство
        /// </summary>
        public void ResyncProductionLicenses()
        {
            Post<EmptyResponse>("reestr/prod_licenses/resync", new { });
        }
    }
}
