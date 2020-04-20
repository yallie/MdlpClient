namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.11. Список прав пользователей учетной системы
    /// </summary>
    public class RightsEnum
    {
        /// <summary>
        /// Управление учетками
        /// </summary>
        public const string MANAGE_ACCOUNTS = "MANAGE_ACCOUNTS";

        /// <summary>
        /// Просмотр учетных записей
        /// </summary>
        public const string VIEW_ACCOUNTS = "VIEW_ACCOUNTS";

        /// <summary>
        /// Загрузка документа
        /// </summary>
        public const string UPLOAD_DOCUMENT = "UPLOAD_DOCUMENT";

        /// <summary>
        /// Информация об исходящем документе
        /// </summary>
        public const string OUTCOME_LIST = "OUTCOME_LIST";

        /// <summary>
        /// Информация о входящих документах
        /// </summary>
        public const string INCOME_LIST = "INCOME_LIST";

        /// <summary>
        /// Получение ссылки на документ по идентификатору
        /// </summary>
        public const string DOWNLOAD_DOCUMENT = "DOWNLOAD_DOCUMENT";

        /// <summary>
        /// Доступ к реестрам (ко всем справочникам)
        /// </summary>
        public const string REESTR_ALL = "REESTR_ALL";

        /// <summary>
        /// Реестр субъектов РФ
        /// </summary>
        public const string REESTR_FEDERAL_SUBJECT = "REESTR_FEDERAL_SUBJECT";

        /// <summary>
        /// Реестр ЕГРЮЛ
        /// </summary>
        public const string REESTR_EGRUL = "REESTR_EGRUL";

        /// <summary>
        /// Реестр ЕГРИП
        /// </summary>
        public const string REESTR_EGRIP = "REESTR_EGRIP";

        /// <summary>
        /// Реестр аккредитованных филиалов и представительств
        /// </summary>
        public const string REESTR_REFP = "REESTR_REFP";

        /// <summary>
        /// Реестр налоговой задолженности
        /// </summary>
        internal const string REESTR_DUES = "REESTR_DUES";

        /// <summary>
        /// Реестр лицензий на производство
        /// </summary>
        public const string REESTR_PROD_LICENSES = "REESTR_PROD_LICENSES";

        /// <summary>
        /// Реестр лицензий на фарм. деятельность
        /// </summary>
        public const string REESTR_PHARM_LICENSES = "REESTR_PHARM_LICENSES";

        /// <summary>
        /// Реестр ЕСКЛП
        /// </summary>
        public const string REESTR_ESKLP = "REESTR_ESKLP";

        /// <summary>
        /// Реестр ГС1 (GS1)
        /// </summary>
        internal const string REESTR_GS1 = "REESTR_GS1";

        /// <summary>
        /// Реестр ФИАС
        /// </summary>
        public const string REESTR_FIAS = "REESTR_FIAS";

        /// <summary>
        /// Просмотр реестра приоритетной оплаты
        /// </summary>
        public const string VIEW_BILLING_PRIORITY_RULES = "VIEW_BILLING_PRIORITY_RULES";

        /// <summary>
        /// Редактирование реестра приоритетной оплаты
        /// </summary>
        public const string MANAGE_BILLING_PRIORITY_RULES = "MANAGE_BILLING_PRIORITY_RULES";

        /// <summary>
        /// Реестр КИЗ
        /// </summary>
        public const string REESTR_SGTIN = "REESTR_SGTIN";

        /// <summary>
        /// Реестр КИЗ для биллинга
        /// </summary>
        public const string REESTR_SGTIN_BILLING = "REESTR_SGTIN_BILLING";

        /// <summary>
        /// Реестр КИЗ и реестр третичных упаковок с учетом текущего владельца
        /// </summary>
        internal const string REESTR_OWNED_SSCC_SGTIN = "REESTR_OWNED_SSCC_SGTIN";

        /// <summary>
        /// Реестр производимых ЛП
        /// </summary>
        public const string REESTR_MED_PRODUCTS = "REESTR_MED_PRODUCTS";

        /// <summary>
        /// Редактирование реестра доверенных контрагентов
        /// </summary>
        public const string MANAGE_TRUSTED_PARTNERS = "MANAGE_TRUSTED_PARTNERS";

        /// <summary>
        /// Просмотр реестра доверенных контрагентов
        /// </summary>
        public const string VIEW_TRUSTED_PARTNERS = "VIEW_TRUSTED_PARTNERS";

        /// <summary>
        /// Редактирование реестра мест деятельности (МД)
        /// </summary>
        public const string MANAGE_BRANCH = "MANAGE_BRANCH";

        /// <summary>
        /// Редактирование реестра складов/мест ответственного хранения СОХ/МОХ
        /// </summary>
        public const string MANAGE_SAFE_WAREHOUSE = "MANAGE_SAFE_WAREHOUSE";

        /// <summary>
        /// Реестр заявок на регистрацию иностранных контрагентов
        /// </summary>
        public const string VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG = "VIEW_REGISTRATION_FOREIGN_COUNTERPARTY_LOG";

        /// <summary>
        /// Управление иностранными контрагентами
        /// </summary>
        public const string MANAGE_FOREIGN_COUNTERPARTY = "MANAGE_FOREIGN_COUNTERPARTY";

        /// <summary>
        /// Управление организацией
        /// </summary>
        internal const string MANAGE_MEMBER = "MANAGE_MEMBER";

        /// <summary>
        /// Реестр контрагентов
        /// </summary>
        public const string REESTR_COUNTERPARTY = "REESTR_COUNTERPARTY";

        /// <summary>
        /// Реестра регистраторов эмиссии/выбытия
        /// </summary>
        public const string REESTR_REGISTRATION_DEVICES = "REESTR_REGISTRATION_DEVICES";

        /// <summary>
        /// Реестр виртуального склада
        /// </summary>
        public const string REESTR_VIRTUAL_STORAGE = "REESTR_VIRTUAL_STORAGE";

        /// <summary>
        /// Финансовая информация
        /// </summary>
        public const string MEMBER_PAYMENT_INFO = "MEMBER_PAYMENT_INFO";

        /// <summary>
        /// Реестр решений о приостановке КИЗ
        /// </summary>
        public const string REESTR_PAUSED_CIRCULATION_DECISION = "REESTR_PAUSED_CIRCULATION_DECISION";

        /// <summary>
        /// Прослеживание документов по отчёту из СУЗ
        /// </summary>
        public const string VIEW_SKZKM_REPORT = "VIEW_SKZKM_REPORT";

        /// <summary>
        /// Просмотр дерева по производственной серии для производителя
        /// </summary>
        internal const string VIEW_BATCH_GRAF = "VIEW_BATCH_GRAF";
    }
}
