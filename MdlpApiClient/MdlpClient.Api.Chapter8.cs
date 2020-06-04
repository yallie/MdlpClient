namespace MdlpApiClient
{
    using System.Linq;
    using System.Net;
    using DataContracts;
    using MdlpApiClient.Toolbox;
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
            RequestRate(0.5); // 56

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
            RequestRate(0.5); // 57

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
            RequestRate(0.5); // 58

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
            RequestRate(0.5); // 61

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
        public GetWarehouseResponse GetWarehouse(string warehouseId)
        {
            RequestRate(0.5); // 62

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
            RequestRate(0.5); // 63

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
        public EntriesResponse<RegistrationAddress> GetAvailableAddresses(string inn = null, string licenseNumber = null)
        {
            RequestRate(0.5); // 64

            return Post<EntriesResponse<RegistrationAddress>>("reestr/warehouses/available_safe_warehouses_addresses", new
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
            RequestRate(0.5); // 69

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
        /// <param name="sgtins">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<SgtinExtended, SgtinFailed> GetSgtins(params string[] sgtins)
        {
            RequestRate(5, "sgtins-by-list"); // 70

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
        /// <param name="sgtins">Список КИЗ для поиска (не более 500 значений)</param>
        /// <returns>Список КИЗ</returns>
        public EntriesFailedResponse<PublicSgtin, string> GetPublicSgtins(params string[] sgtins)
        {
            RequestRate(1.5); // 88, сказано 1

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
            RequestRate(0.5); // 71

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
            RequestRate(0.5); // 96

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
            RequestRate(0.5); // 90

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
            RequestRate(1); // 99

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
        public SsccHierarchyResponse<SsccInfo> GetSsccHierarchy(string sscc)
        {
            RequestRate(5); // 73

            return Get<SsccHierarchyResponse<SsccInfo>>("reestr/sscc/{sscc}/hierarchy", new[]
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
            RequestRate(6); // 74, сказано 5

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
        /// 8.4.3. Метод для получения информации о полной иерархии вложенности третичной упаковки
        /// </summary>
        /// <param name="sscc">Идентификационный код третичной упаковки</param>
        /// <returns>Список КИЗ, непосредственно вложенных в указанную третичную упаковку</returns>
        public SsccFullHierarchyResponse<HierarchySsccInfo> GetSsccFullHierarchy(string sscc)
        {
            RequestRate(35); // 100, сказано 30

            var result = Get<SsccFullHierarchyResponse<HierarchySsccInfoInternal>>("reestr/sscc/{sscc}/full-hierarchy", new[]
            {
                new Parameter("sscc", sscc, ParameterType.UrlSegment),
            });

            // sort child records and convert to real HierarchySsccInfo items
            return new SsccFullHierarchyResponse<HierarchySsccInfo>
            {
                Up = HierarchySsccInfoInternal.Convert(result.Up),
                Down = HierarchySsccInfoInternal.Convert(result.Down),
            };
        }

        /// <summary>
        /// 8.4.4. Метод для получения информации о полной иерархии 
        /// вложенности третичной упаковки для нескольких SSCC
        /// </summary>
        /// <param name="ssccs">Список идентификационный код третичной упаковки</param>
        /// <returns>Список КИЗ, непосредственно вложенных в указанную третичную упаковку</returns>
        public SsccFullHierarchyResponse<HierarchySsccInfo>[] GetSsccFullHierarchy(string[] ssccs)
        {
            if (ssccs.IsNullOrEmpty())
            {
                return new SsccFullHierarchyResponse<HierarchySsccInfo>[0];
            }

            RequestRate(35); // 101, сказано 30

            var ssccList = string.Join(",", ssccs);
            var result = Get<SsccFullHierarchyResponse<HierarchySsccInfoInternal>[]>("reestr/sscc/full-hierarchy", new[]
            {
                new Parameter("sscc", ssccList, ParameterType.QueryString),
            });

            // sort child records and convert to real HierarchySsccInfo items
            return result.Select(r => new SsccFullHierarchyResponse<HierarchySsccInfo>
            {
                Up = HierarchySsccInfoInternal.Convert(r.Up),
                Down = HierarchySsccInfoInternal.Convert(r.Down),
            })
            .ToArray();
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
            RequestRate(0.5); // 75

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
            RequestRate(0.5); // 76

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
            RequestRate(5); // 95

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
            RequestRate(1); // 89

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
            RequestRate(0.5); // 77

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
            RequestRate(0.5); // 78

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
            RequestRate(0.5); // 79

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
            RequestRate(0.5); // 80

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
            RequestRate(0.5); // 81

            return Post<EntriesResponse<TrustedPartnerEntry>>("/reestr/trusted_partners/filter", new
            {
                filter = filter ?? new TrustedPartnerFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.8.1. Метод фильтрации по субъектам обращения
        /// </summary>
        /// <typeparam name="T">Тип субъекта обращения</typeparam>
        /// <param name="filter">Фильтр для поиска субъектов обращения</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых субъектов обращения</param>
        /// <param name="count">Количество записей в списке возвращаемых субъектов обращения</param>
        /// <returns>Список субъектов обращения</returns>
        private EntriesFilteredResponse<T> GetPartners<T>(PartnerFilter filter, int startFrom, int count)
        {
            RequestRate(0.5); // 82

            return Post<EntriesFilteredResponse<T>>("reestr_partners/filter", new
            {
                filter = filter ?? new PartnerFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.8.1. Метод фильтрации по иностранным субъектам обращения
        /// </summary>
        /// <param name="filter">Фильтр для поиска субъектов обращения</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых субъектов обращения</param>
        /// <param name="count">Количество записей в списке возвращаемых субъектов обращения</param>
        /// <returns>Список субъектов обращения</returns>
        public EntriesFilteredResponse<ForeignCounterparty> GetForeignPartners(PartnerFilter filter, int startFrom, int count)
        {
            RequestRate(0.5); // 82?

            // запрос иностранных контрагентов
            filter = filter ?? new PartnerFilter();
            filter.RegEntityType = RegEntityTypeEnum.FOREIGN_COUNTERPARTY;

            return GetPartners<ForeignCounterparty>(filter, startFrom, count);
        }

        /// <summary>
        /// 8.8.1. Метод фильтрации по местным субъектам обращения
        /// </summary>
        /// <param name="filter">Фильтр для поиска субъектов обращения</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых субъектов обращения</param>
        /// <param name="count">Количество записей в списке возвращаемых субъектов обращения</param>
        /// <returns>Список субъектов обращения</returns>
        public EntriesFilteredResponse<RegistrationEntry> GetLocalPartners(PartnerFilter filter, int startFrom, int count)
        {
            // запрос местных контрагентов
            filter = filter ?? new PartnerFilter();
            if (filter.RegEntityType == RegEntityTypeEnum.FOREIGN_COUNTERPARTY)
            {
                throw new MdlpException(HttpStatusCode.BadRequest, "Use GetForeignPartners method to return foreign counterparties", null, null);
            }
            else if (filter.RegEntityType == 0)
            {
                filter.RegEntityType = RegEntityTypeEnum.RESIDENT;
            }

            return GetPartners<RegistrationEntry>(filter, startFrom, count);
        }

        /// <summary>
        /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
        /// </summary>
        /// <returns>Описание организации</returns>
        public Member GetCurrentMember()
        {
            RequestRate(0.5); // 83

            var member = Get<MemberResponse>("members/current");
            return member != null ? member.Member : null;
        }

        /// <summary>
        /// 8.9.2. Метод для изменения данных организации, в которой зарегистрирован текущий пользователь
        /// </summary>
        public void UpdateCurrentMember(MemberOptions options)
        {
            Put("members/current", options);
        }

        /// <summary>
        /// 8.9.3. Метод для получения информации о лицевых счетах
        /// </summary>
        public BillingAccount[] GetCurrentBillingInfo()
        {
            var accounts = Get<BillingAccountResponse>("members/current/billing/info");
            return accounts != null ? accounts.Accounts : null;
        }

        /// <summary>
        /// 8.10.1. Фильтрация по реестру регистраторов эмиссии
        /// </summary>
        /// <param name="filter">Фильтр для поиска регистраторов эмиссии</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых регистраторов эмиссии</param>
        /// <param name="count">Количество записей в списке возвращаемых регистраторов эмиссии</param>
        /// <returns>Список регистраторов эмиссии</returns>
        public EntriesResponse<EmissionDeviceInfoEntry> GetEmissionDevices(EmissionDeviceFilter filter, int startFrom, int count)
        {
            RequestRate(1); // 85

            return Post<EntriesResponse<EmissionDeviceInfoEntry>>("reestr/registration-devices/emission/filter", new
            {
                filter = filter ?? new EmissionDeviceFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.10.2. Фильтрация по реестру регистраторов выбытия
        /// </summary>
        /// <param name="filter">Фильтр для поиска регистраторов выбытия</param>
        /// <param name="startFrom">Индекс первой записи в списке возвращаемых регистраторов выбытия</param>
        /// <param name="count">Количество записей в списке возвращаемых регистраторов выбытия</param>
        /// <returns>Список регистраторов выбытия</returns>
        public EntriesResponse<WithdrawalDeviceInfoEntry> GetWithdrawalDevices(WithdrawalDeviceFilter filter, int startFrom, int count)
        {
            RequestRate(1); // 86

            return Post<EntriesResponse<WithdrawalDeviceInfoEntry>>("reestr/registration-devices/withdrawal/filter", new
            {
                filter = filter ?? new WithdrawalDeviceFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.11.1. Фильтрация по реестру виртуального склада
        /// </summary>
        /// <param name="filter">Фильтр реестра виртуального склада</param>
        /// <param name="startFrom">Индекс первой записи</param>
        /// <param name="count">Количество записей в реестре</param>
        /// <returns>Список остатков</returns>
        public EntriesResponse<VirtualStorageEntry> GetVirtualStorage(VirtualStorageFilter filter, int startFrom, int count)
        {
            RequestRate(1); // 87

            return Post<EntriesResponse<VirtualStorageEntry>>("reestr/virtual-storage/filter", new
            {
                filter = filter ?? new VirtualStorageFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.12.1. Фильтрация по реестру решений о приостановке КИЗ
        /// </summary>
        /// <param name="filter">Фильтр реестра решений о приостановке КИЗ</param>
        /// <param name="startFrom">Индекс первой записи</param>
        /// <param name="count">Количество записей в реестре</param>
        /// <returns>Список решений о приостановке КИЗ</returns>
        public EntriesResponse<PausedCirculationDecision> GetPausedCirculationDecisions(PausedCirculationDecisionFilter filter, int startFrom, int count)
        {
            return Post<EntriesResponse<PausedCirculationDecision>>("reestr/paused-circulation-decisions/filter", new
            {
                filter = filter ?? new PausedCirculationDecisionFilter(),
                start_from = startFrom,
                count = count,
            });
        }

        /// <summary>
        /// 8.12.2. Получение перечня КИЗ по конкретному решению о приостановке
        /// </summary>
        /// <param name="haltId">Идентификатор решения о приостановке КИЗ</param>
        /// <param name="startFrom">Индекс первой записи</param>
        /// <param name="count">Количество записей</param>
        /// <returns>Список приостановленных КИЗ</returns>
        public EntriesResponse<PausedCirculationSgtin> GetPausedCirculationSgtins(string haltId, int startFrom, int count)
        {
            RequestRate(0.5); // 72

            return Post<EntriesResponse<PausedCirculationSgtin>>("reestr/paused-circulation-decisions/{halt_id}/sgtins/filter", new
            {
                start_from = startFrom,
                count = count,
            },
            new[]
            {
                new Parameter("halt_id", haltId, ParameterType.UrlSegment),
            });
        }

        /// <summary>
        /// 8.13.1. Получение сводной информации распределения ЛП
        /// </summary>
        /// <param name="gtin">Код GTIN лекарственного препарата</param>
        /// <param name="batch">Номер производственной серии</param>
        /// <returns>Список решений о приостановке КИЗ</returns>
        public EntriesResponse<ShortDistribution> GetBatchShortDistribution(string gtin, string batch)
        {
            return Post<EntriesResponse<ShortDistribution>>("reestr/batches/short-distribution", new
            {
                gtin = gtin,
                batches = new[]
                {
                    batch
                },
            });
        }
    }
}
