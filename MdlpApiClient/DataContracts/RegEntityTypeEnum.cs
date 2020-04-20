namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 8.8.1. Метод фильтрации по субъектам обращения
    /// Типы зарегистрированных участников
    /// </summary>
    public class RegEntityTypeEnum
    {
        /// <summary>
        /// Тип участника: 1 — резидент РФ
        /// </summary>
        public const int RESIDENT = 1;

        /// <summary>
        /// Тип участника: 2 — представительство иностранного держателя регистрационного удостоверения
        /// <summary>
        public const int FOREIGN_REGHOLDER_BRANCH = 2;

        /// <summary>
        /// Тип участника: 3 — иностранный держатель регистрационного удостоверения
        /// <summary>
        public const int FOREIGN_REGHOLDER = 3;

        /// <summary>
        /// Тип участника: 8 — иностранный контрагент
        /// <summary>
        public const int FOREIGN_COUNTERPARTY = 8;    }
}
