namespace MdlpApiClient
{
    using DataContracts;

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
    }
}
