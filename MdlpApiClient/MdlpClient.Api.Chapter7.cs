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
            RequestRate(0.5); // 46

            return Get<EgrulRegistryResponse>("reestr/egrul");
        }

        /// <summary>
        /// 7.2.1. Получение данных записи ЕГРИП
        /// </summary>
        /// <returns>Данные из реестра ЕГРИП</returns>
        public EgripRegistryResponse GetEgripRegistryEntry()
        {
            RequestRate(0.5); // 47

            return Get<EgripRegistryResponse>("reestr/egrip");
        }

        /// <summary>
        /// 7.3.1. Получение записи реестра РАФП (реестра аккредитованных филиалов и представительств)
        /// </summary>
        /// <returns>Данные из реестра РАФП</returns>
        public RafpRegistryResponse GetRafpRegistryEntry()
        {
            RequestRate(0.5); // 48

            return Get<RafpRegistryResponse>("reestr/rafp");
        }

        /// <summary>
        /// 7.5.1. Получение объекта ФИАС по идентификатору адресного объекта
        /// </summary>
        /// <param name="addressId">Идентификатор адресного объекта</param>
        /// <returns>Данные из реестра ФИАС</returns>
        public FiasAddressObject GetFiasAddressObject(string addressId)
        {
            RequestRate(0.5); // 50

            return Get<FiasAddressObject>("reestr/fias/addrobj/{addrobj}", new[]
            {
                new Parameter("addrobj", addressId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 7.5.2. Получение объекта ФИАС по идентификатору дома
        /// </summary>
        /// <param name="houseGuid">Идентификатор дома</param>
        /// <returns>Данные из реестра ФИАС</returns>
        public FiasHouseObject GetFiasHouseObject(string houseGuid)
        {
            RequestRate(0.5); // 51

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
            RequestRate(0.5); // 52

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
        public LicenseEntry[] GetProductionLicenses()
        {
            RequestRate(0.5); // 53

            return Get<List<LicenseEntry>>("reestr/prod_licenses").ToArray();
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
            RequestRate(0.5); // 94

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
            RequestRate(86400); // 91: сутки

            Post<EmptyResponse>("reestr/prod_licenses/resync", new { });
        }

        /// <summary>
        /// 7.7.1. Получение информации о лицензиях на фарм. деятельность
        /// </summary>
        /// <returns>Список лицензий</returns>
        public LicenseEntry[] GetPharmacyLicenses()
        {
            RequestRate(0.5); // 54

            return Get<List<LicenseEntry>>("reestr/pharm_licenses").ToArray();
        }

        /// <summary>
        /// 7.7.2. Метод фильтрации лицензий на фарм. деятельность
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру лицензий на фарм. деятельность</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых лицензий на фарм. деятельность</param>
        /// <param name="count">Количество записей в списке возвращаемых лицензий на фарм. деятельность</param>
        /// <returns>Список лицензий</returns>
        public EntriesResponse<LicenseEntry> GetPharmacyLicenses(LicenseApiFilter filter, int startFrom, int count)
        {
            RequestRate(0.5); // 93

            return Post<EntriesResponse<LicenseEntry>>("reestr/pharm_licenses", new
            {
                filter = filter ?? new LicenseApiFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 7.7.3. Метод для актуализации данных текущего участника из реестра лицензий на фарм.деятельность
        /// </summary>
        public void ResyncPharmacyLicenses()
        {
            RequestRate(86400); // 92: сутки

            Post<EmptyResponse>("reestr/pharm_licenses/resync", new { });
        }

        /// <summary>
        /// 7.8.1. Метод для получения информации о всех местах осуществления
        /// деятельности и местах ответственного хранения участника
        /// </summary>
        /// <returns>Список адресов</returns>
        /// <remarks>
        /// Ошибка в документации: сказано, что возвращается <see cref="AddressEntry"/>.
        /// </remarks>
        public EntriesResponse<AddressEntry> GetCurrentAddresses()
        {
            RequestRate(0.5); // 65

            return Get<EntriesResponse<AddressEntry>>("reestr/address/all");
        }

        /// <summary>
        /// 7.9.1. Метод для получения списка стран
        /// </summary>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых стран</param>
        /// <param name="count">Количество записей в списке возвращаемых стран</param>
        /// <returns>Список стран</returns>
        public EntriesResponse<CountryInfo> GetCountries(int startFrom, int count)
        {
            RequestRate(0.5); // 66

            return Post<EntriesResponse<CountryInfo>>("reestr/area/countries", new
            {
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 7.9.2. Метод для получения списка субъектов РФ
        /// </summary>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых субъектов РФ</param>
        /// <param name="count">Количество записей в списке возвращаемых субъектов РФ</param>
        /// <returns>Список субъектов РФ</returns>
        public EntriesResponse<Region> GetRegions(int startFrom, int count)
        {
            RequestRate(0.5); // 67

            return Post<EntriesResponse<Region>>("reestr/area/regions", new
            {
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 7.10.1. Фильтрация по реестру ЕСКЛП (Единый справочник-каталог лекарственных препаратов)
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру ЕСКЛП</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых записей реестра ЕСКЛП</param>
        /// <param name="count">Количество записей в списке возвращаемых записей реестра ЕСКЛП</param>
        /// <returns>Список записей реестра ЕСКЛП</returns>
        public EntriesResponse<EsklpInfo> GetEsklpInfo(EsklpFilter filter, int startFrom, int count)
        {
            RequestRate(0.5); // 68

            return Post<EntriesResponse<EsklpInfo>>("reestr/esklp/filter", new
            {
                filter = filter ?? new EsklpFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 7.11.1. Фильтрация по реестру мест таможенного контроля
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру мест таможенного контроля</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест таможенного контроля</param>
        /// <param name="count">Количество записей в списке возвращаемых мест таможенного контроля</param>
        /// <returns>Список мест таможенного контроля</returns>
        public EntriesResponse<CustomsPointsInfoEntry> GetCustomsPoints(CustomsPointsFilter filter, int startFrom, int count)
        {
            RequestRate(0.5); // 84

            return Post<EntriesResponse<CustomsPointsInfoEntry>>("reestr/customs_points/filter", new
            {
                filter = filter ?? new CustomsPointsFilter(),
                start_from = startFrom,
                count = count,
            });
        }
    }
}
