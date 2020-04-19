namespace MdlpApiClient
{
    using DataContracts;
    using RestSharp;

    /// <remarks>
    /// Strongly typed REST API methods. Chapter 8: MDLP information.
    /// </remarks>
    partial class MdlpClient
    {
        /// <summary>
        /// 8.1.2. Метод для поиска информации о местах осуществления деятельности по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест осуществления деятельности</returns>
        public EntriesResponse<BranchEntry> GetBranches(BranchFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<BranchEntry>>("reestr/branches/filter", new
            {
                filter = filter ?? new BranchFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.1.3. Получение информации о конкретном месте осуществления деятельности
        /// </summary>
        public GetBranchResponse GetBranch(string branchId)
        {
            return Get<GetBranchResponse>("reestr/branches/{branch_id}", new[]
            {
                new Parameter("branch_id", branchId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 8.1.4. Метод для регистрация места осуществления деятельности
        /// </summary>
        public string RegisterBranch(Address address)
        {
            var branch = Post<GetBranchResponse>("reestr/branches/register", new
            {
                branch_address = address
            });

            return branch.BranchID;
        }

        /// <summary>
        /// 8.2.2. Метод для поиска информации о местах ответственного хранения по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска мест осуществления деятельности</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых мест</param>
        /// <param name="count">Количество записей в списке возвращаемых мест</param>
        /// <returns>Список мест ответственного хранения</returns>
        public EntriesResponse<WarehouseEntry> GetWarehouses(WarehouseFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<WarehouseEntry>>("reestr/warehouses/filter", new
            {
                filter = filter ?? new WarehouseFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.2.3. Получение информации о конкретном месте ответственного хранения
        /// </summary>
        public GetWarehouseResponse GetWarehouses(string warehouseId)
        {
            return Get<GetWarehouseResponse>("reestr/warehouses/{warehouse_id}", new[]
            {
                new Parameter("warehouse_id", warehouseId, ParameterType.UrlSegment)
            });
        }

        /// <summary>
        /// 8.2.4. Метод для регистрация места ответственного хранения
        /// </summary>
        public string RegisterWarehouse(string warehouseOrgInn, Address address)
        {
            var warehouse = Post<RegisterWarehouseResponse>("reestr/warehouses/register", new
            {
                warehouse_org_inn = warehouseOrgInn,
                warehouse_address = address
            });

            return warehouse.WarehouseID;
        }

        /// <summary>
        /// 8.2.5. Метод получения информации об адресах искомого участника,
        /// для регистрации мест ответственного хранения или отправки документов.
        /// </summary>
        /// <param name="inn">ИНН (необязательно)</param>
        /// <param name="licenseNumber">Номер лицензии (необязательно)</param>
        public GetAvailableAddressesResponse GetAvailableAddresses(string inn = null, string licenseNumber = null)
        {
            return Post<GetAvailableAddressesResponse>("reestr/warehouses/available_safe_warehouses_addresses", new
            {
                inn = inn,
                licence_number = licenseNumber,
            });
        }

        /// <summary>
        /// 8.3.1. Метод для поиска по реестру КИЗ
        /// </summary>
        /// <param name="filter">Фильтр для поиска по реестру КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinExtended> GetSgtins(SgtinFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinExtended>>("reestr/sgtin/filter", new
            {
                filter = filter ?? new SgtinFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.2. Метод поиска по реестру КИЗ по списку значений
        /// </summary>
        /// <param name="filters">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<SgtinExtended, SgtinFailed> GetSgtins(string[] sgtins)
        {
            return Post<EntriesFailedResponse<SgtinExtended, SgtinFailed>>("reestr/sgtin/sgtins-by-list", new
            {
                filter = new
                {
                    sgtins = sgtins
                },
            });
        }

        /// <summary>
        /// 8.3.3. Метод поиска по общедоступному реестру КИЗ по списку значений
        /// </summary>
        /// <param name="filters">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<PublicSgtin, string> GetPublicSgtins(string[] sgtins)
        {
            return Post<EntriesFailedResponse<PublicSgtin, string>>("reestr/sgtin/public/sgtins-by-list", new
            {
                filter = new
                {
                    sgtins = sgtins
                },
            });
        }

        /// <summary>
        /// 8.3.4. Метод для получения детальной информации о КИЗ и связанным с ним ЛП
        /// </summary>
        /// <param name="sgtin">КИЗ для поиска</param>
        /// <returns>Подробная информация КИЗ и ЛП</returns>
        public GetSgtinResponse GetSgtin(string sgtin)
        {
            return Get<GetSgtinResponse>("reestr/sgtin/{sgtin}", new[]
            {
                new Parameter("sgtin", sgtin, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.3.5. Метод для поиска по реестру КИЗ всех записей со статусом 'Оборот приостановлен'
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinExtended> GetSgtinsOnHold(SgtinOnHoldFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinExtended>>("reestr/sgtin/on_hold", new
            {
                filter = filter ?? new SgtinOnHoldFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.6. Метод для поиска по реестру КИЗ записей, ожидающих
        /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ)
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinKktAwaitingWithdrawal> GetSgtinsKktAwaitingWithdrawal(SgtinAwaitingWithdrawalFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinKktAwaitingWithdrawal>>("reestr/sgtin/kkt/awaiting-withdrawal/filter", new
            {
                filter = filter ?? new SgtinAwaitingWithdrawalFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.3.7. Метод для поиска по реестру КИЗ записей,
        /// ожидающих вывода из оборота через РВ
        /// </summary>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ</returns>
        public EntriesResponse<SgtinDeviceAwaitingWithdrawal> GetSgtinsDeviceAwaitingWithdrawal(SgtinAwaitingWithdrawalFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<SgtinDeviceAwaitingWithdrawal>>("reestr/sgtin/device/awaiting-withdrawal/filter", new
            {
                filter = filter ?? new SgtinAwaitingWithdrawalFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.4.1. Метод для получения информации об иерархии вложенности третичной упаковки
        /// </summary>
        /// <param name="sscc">Идентификационный код третичной упаковки</param>
        /// <returns>Подробная информация КИЗ и ЛП</returns>
        public GetSsccHierarchyResponse GetSsccHierarchy(string sscc)
        {
            return Get<GetSsccHierarchyResponse>("reestr/sscc/{sscc}/hierarchy", new[]
            {
                new Parameter("sscc", sscc, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.4.2. Метод для получения информации о КИЗ, вложенных в третичную упаковку
        /// </summary>
        /// <param name="sscc">Идентификационный код третичной упаковки</param>
        /// <param name="filter">Фильтр для поиска КИЗ</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых КИЗ</param>
        /// <param name="count">Количество записей в списке возвращаемых КИЗ</param>
        /// <returns>Список КИЗ, непосредственно вложенных в указанную третичную упаковку</returns>
        public GetSsccSgtinsResponse GetSsccSgtins(string sscc, SsccSgtinsFilter filter, int startFrom, int count)
        {
            return Post<GetSsccSgtinsResponse>("reestr/sscc/{sscc}/sgtins", new
            {
                sscc = sscc,
                filter = filter ?? new SsccSgtinsFilter(),
                start_from = startFrom,
                count = count,
            },
            new[]
            {
                new Parameter("sscc", sscc, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.5.1. Метод для получения информации из реестра производимых организацией ЛП
        /// </summary>
        /// <param name="filter">Фильтр для поиска ЛП</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых ЛП</param>
        /// <param name="count">Количество записей в списке возвращаемых ЛП</param>
        /// <returns>Список ЛП</returns>
        public EntriesResponse<MedProduct> GetCurrentMedProducts(MedProductsFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<MedProduct>>("reestr/med_products/current", new
            {
                filter = filter ?? new MedProductsFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.5.2. Метод для получения детальной информации о производимом организацией ЛП
        /// </summary>
        /// <param name="gtin">Код GTIN ЛП</param>
        /// <returns>Описание ЛП</returns>
        public MedProduct GetCurrentMedProduct(string gtin)
        {
            return Get<MedProduct>("reestr/med_products/{gtin}", new[]
            {
                new Parameter("gtin", gtin, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.5.3. Метод для поиска публичной информации в реестре производимых ЛП
        /// </summary>
        /// <param name="filter">Фильтр для поиска ЛП</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых ЛП</param>
        /// <param name="count">Количество записей в списке возвращаемых ЛП</param>
        /// <returns>Список ЛП</returns>
        public EntriesResponse<MedProductPublic> GetPublicMedProducts(MedProductsFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<MedProductPublic>>("reestr/med_products/public/filter", new
            {
                filter = filter ?? new MedProductsFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.5.4. Метод для получения публичной информации о производимом ЛП
        /// </summary>
        /// <remarks>
        /// По-моему, этот метод возвращает меньше данных, чем 8.5.3.
        /// Например, регистрационный номер и статус не возвращает.
        /// Зато метод 8.5.3 почему-то не возвращает владельца лицензии.
        /// </remarks>
        /// <param name="gtin">Код GTIN ЛП</param>
        /// <returns>Описание ЛП</returns>
        public MedProductPublic GetPublicMedProduct(string gtin)
        {
            return Get<MedProductPublic>("reestr/med_products/public/{gtin}", new[]
            {
                new Parameter("gtin", gtin, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.6.1. Метод для регистрации иностранного контрагента
        /// </summary>
        /// <param name="itin">ИТИН контрагента</param>
        /// <param name="name">Наименование субъекта обращения</param>
        /// <param name="address">Адрес субъекта обращения</param>
        /// <returns>Идентификатор контрагента</returns>
        public string RegisterForeignCounterparty(string itin, string name, ForeignAddress address)
        {
            var counterparty = Post<RegisterForeignCounterpartyResponse>("reestr/foreign_counterparty/register", new
            {
                counterparty_itin = itin,
                counterparty_name = name,
                counterparty_address = address,
            });

            return counterparty.CounterpartyID;
        }

        /// <summary>
        /// 8.6.2. Метод для просмотра заявок на регистрацию иностранных контрагентов
        /// </summary>
        /// <param name="filter">Фильтр для поиска иностранных контрагентов</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых иностранных контрагентов</param>
        /// <param name="count">Количество записей в списке возвращаемых иностранных контрагентов</param>
        /// <returns>Список иностранных контрагентов</returns>
        public EntriesResponse<ForeignCounterpartyEntry> GetForeignCounterparties(ForeignCounterpartyFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<ForeignCounterpartyEntry>>("reestr/foreign_counterparty/filter", new
            {
                filter = filter ?? new ForeignCounterpartyFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.7.1. Метод добавления доверенного контрагента или контрагентов
        /// </summary>
        /// <param name="partnerIds">Идентификаторы субъектов или ИНН партнеров</param>
        public void AddTrustedPartners(params string[] partnerIds)
        {
            Post("reestr/trusted_partners/add", new
            {
                trusted_partners = partnerIds
            });
        }

        /// <summary>
        /// 8.7.2. Метод удаления доверенного контрагента или контрагентов
        /// </summary>
        /// <param name="partnerIds">Идентификаторы субъектов или ИНН партнеров</param>
        public void DeleteTrustedPartners(params string[] partnerIds)
        {
            Post("reestr/trusted_partners/delete", new
            {
                trusted_partners = partnerIds
            });
        }

        /// <summary>
        /// 8.7.3. Метод фильтрации доверенных контрагентов
        /// </summary>
        /// <param name="filter">Фильтр для поиска доверенных контрагентов</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых доверенных контрагентов</param>
        /// <param name="count">Количество записей в списке возвращаемых доверенных контрагентов</param>
        /// <returns>Список доверенных контрагентов</returns>
        public EntriesResponse<TrustedPartnerEntry> GetTrustedPartners(TrustedPartnerFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<TrustedPartnerEntry>>("/reestr/trusted_partners/filter", new
            {
                filter = filter ?? new TrustedPartnerFilter(),
                start_from = startFrom,
                count = count,
            });
        }
    }
}
