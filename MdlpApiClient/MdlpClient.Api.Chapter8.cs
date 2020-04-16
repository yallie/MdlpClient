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
        public GetBranchesResponse GetBranches(BranchFilter filter, int startFrom, int count)
        {
            return Post<GetBranchesResponse>("reestr/branches/filter", new
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
        public GetWarehousesResponse GetWarehouses(WarehouseFilter filter, int startFrom, int count)
        {
            return Post<GetWarehousesResponse>("reestr/warehouses/filter", new
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
        public GetSgtinResponse GetSgtins(SgtinFilter filter, int startFrom, int count)
        {
            return Post<GetSgtinResponse>("reestr/sgtin/filter", new
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
        public GetSgtinResponse GetSgtins(string[] sgtins)
        {
            return Post<GetSgtinResponse>("reestr/sgtin/sgtins-by-list", new
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
        public GetPublicSgtinResponse GetPublicSgtins(string[] sgtins)
        {
            return Post<GetPublicSgtinResponse>("reestr/sgtin/public/sgtins-by-list", new
            {
                filter = new
                {
                    sgtins = sgtins
                },
            });
        }
    }
}
