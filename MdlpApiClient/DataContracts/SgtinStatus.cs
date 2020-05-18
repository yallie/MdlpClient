namespace MdlpApiClient.DataContracts
{
    /// <summary>
    /// 4.43. Список возможных статусов КИЗ
    /// Таблица 39. Статусы КИЗ
    /// </summary>
    public class SgtinStatus
    {
        /// <summary>
        /// Ожидает выпуска.
        /// </summary>
        public const string MARKED = "marked";

        /// <summary>
        /// Отобран образец.
        /// </summary>
        public const string LP_SAMPLED = "lp_sampled";

        /// <summary>
        /// Передан на уничтожение.
        /// </summary>
        public const string MOVED_FOR_DISPOSAL = "moved_for_disposal";

        /// <summary>
        /// Уничтожен.
        /// </summary>
        public const string DISPOSED = "disposed";

        /// <summary>
        /// Выведен из оборота.
        /// </summary>
        public const string OUT_OF_CIRCULATION = "out_of_circulation";

        /// <summary>
        /// Ожидает подтверждения получения собственником.
        /// </summary>
        public const string TRANSFERED_TO_OWNER = "transfered_to_owner";

        /// <summary>
        /// Отгружен в РФ.
        /// </summary>
        public const string SHIPPED = "shipped";

        /// <summary>
        /// Ввезен на территорию РФ.
        /// </summary>
        public const string ARRIVED = "arrived";

        /// <summary>
        /// Задекларирован.
        /// </summary>
        public const string DECLARED = "declared";

        /// <summary>
        /// В обороте.
        /// </summary>
        public const string IN_CIRCULATION = "in_circulation";

        /// <summary>
        /// Отгружен.
        /// </summary>
        public const string IN_REALIZATION = "in_realization";

        /// <summary>
        /// Оборот приостановлен.
        /// </summary>
        public const string PAUSED_CIRCULATION = "paused_circulation";

        /// <summary>
        /// Продан в розницу.
        /// </summary>
        public const string IN_SALE = "in_sale";

        /// <summary>
        /// Отпущен по льготному рецепту.
        /// </summary>
        public const string IN_DISCOUNT_PRESCRIPTION_SALE = "in_discount_prescription_sale";

        /// <summary>
        /// Выдан для медицинского применения.
        /// </summary>
        public const string IN_MEDICAL_USE = "in_medical_use";

        /// <summary>
        /// Перемаркирован.
        /// </summary>
        public const string RELABELED = "relabeled";

        /// <summary>
        /// Реэкспорт.
        /// </summary>
        public const string REEXPORTED = "reexported";

        /// <summary>
        /// Ожидает передачи собственнику.
        /// </summary>
        public const string RELEASED_CONTRACT = "released_contract";

        /// <summary>
        /// • для типа эмиссии 3 — Ожидает отгрузки в РФ
        /// • для типа эмиссии 4 — Маркирован в ЗТК.
        /// </summary>
        public const string RELEASED_FOREIGN = "released_foreign";

        /// <summary>
        /// Отгружен на незарегистрированное место деятельности.
        /// </summary>
        public const string MOVED_TO_UNREGISTERED = "moved_to_unregistered";

        /// <summary>
        /// Срок годности истек.
        /// </summary>
        public const string EXPIRED = "expired";

        /// <summary>
        /// Ожидает подтверждения смены собственника.
        /// </summary>
        public const string CHANGE_OWNER = "change_owner";

        /// <summary>
        /// Ожидает подтверждения получения новым владельцем.
        /// </summary>
        public const string CHANGE_OWNER_STATE_GOV = "change_owner_state_gov";

        /// <summary>
        /// Ожидает подтверждения возврата приостановленных лекарственных препаратов.
        /// </summary>
        public const string CONFIRM_RETURN_PAUSED = "confirm_return_paused";

        /// <summary>
        /// Выведен из оборота (накопленный в рамках эксперимента).
        /// </summary>
        public const string EXPERIMENT_OUTBOUND = "experiment_outbound";

        /// <summary>
        /// Частично выдан для медицинского применения.
        /// </summary>
        public const string IN_PARTIAL_MEDICAL_USE = "in_partial_medical_use";

        /// <summary>
        /// Частично продан в розницу.
        /// </summary>
        public const string IN_PARTIAL_SALE = "in_partial_sale";

        /// <summary>
        /// Частично отпущен по льготному рецепту.
        /// </summary>
        public const string IN_PARTIAL_DISCOUNT_PRESCRIPTION_SALE = "in_partial_discount_prescription_sale";

        /// <summary>
        /// Отгружен в ЕАЭС.
        /// </summary>
        public const string MOVED_TO_EEU = "moved_to_eeu";

        /// <summary>
        /// Принят на склад из ЗТК.
        /// </summary>
        public const string MOVED_TO_WAREHOUSE = "moved_to_warehouse";

        /// <summary>
        /// Эмитирован.
        /// </summary>
        public const string EMISSION = "emission";

        /// <summary>
        /// Продан в розницу с использованием ККТ с ошибкой.
        /// </summary>
        public const string OFD_RETAIL_ERROR = "ofd_retail_error";

        /// <summary>
        /// Отпущен по льготному рецепту с использованием ККТ с ошибкой.
        /// </summary>
        public const string OFD_DISCOUNT_PRESCRIPTION_ERROR = "ofd_discount_prescription_error";

        /// <summary>
        /// Ожидает подтверждения получения собственником до ввода в оборот.
        /// </summary>
        public const string TRANSFERRED_FOR_RELEASE = "transferred_for_release";

        /// <summary>
        /// Ожидает ввода в оборот собственником.
        /// </summary>
        public const string WAITING_FOR_RELEASE = "waiting_for_release";

        /// <summary>
        /// Эмитирован.
        /// </summary>
        public const string EMITTED = "emitted";

        /// <summary>
        /// Ожидает выпуска, не оплачен.
        /// </summary>
        public const string MARKED_NOT_PAID = "marked_not_paid";

        /// <summary>
        /// • для типа эмиссии 3 — Ожидает отгрузки в РФ, не оплачен
        /// • для типа эмиссии 4 — Маркирован в ЗТК, не оплачен
        /// </summary>
        public const string RELEASED_FOREIGN_NOT_PAID = "released_foreign_not_paid";

        /// <summary>
        /// Истек срок ожидания оплаты.
        /// </summary>
        public const string EXPIRED_NOT_PAID = "expired_not_paid";

        /// <summary>
        /// Эмитирован, готов к использованию.
        /// </summary>
        public const string EMITTED_PAID = "emitted_paid";

        /// <summary>
        /// Отпущен по льготному рецепту с использованием РВ с ошибкой.
        /// </summary>
        public const string DISCOUNT_PRESCRIPTION_ERROR = "discount_prescription_error";

        /// <summary>
        /// Выдан для медицинского применения с использованием РВ с ошибкой.
        /// </summary>
        public const string MED_CARE_ERROR = "med_care_error";

        /// <summary>
        /// Принят на склад из ЗТК.
        /// </summary>
        public const string DECLARED_WAREHOUSE = "declared_warehouse";

        /// <summary>
        /// Передан для маркировки в ЗТК.
        /// </summary>
        public const string TRANSFERRED_TO_CUSTOMS = "transferred_to_customs";

        /// <summary>
        /// Ожидает подтверждения импортером.
        /// </summary>
        public const string TRANSFERRED_TO_IMPORTER = "transferred_to_importer";

        /// <summary>
        /// В арбитраже.
        /// </summary>
        public const string IN_ARBITRATION = "in_arbitration";

        /// <summary>
        /// Ожидает подтверждения.
        /// </summary>
        public const string WAITING_CONFIRMATION = "waiting_confirmation";

        /// <summary>
        /// Ожидает подтверждения возврата.
        /// </summary>
        public const string TRANSFER_TO_PRODUCTION = "transfer_to_production";

        /// <summary>
        /// Ожидает подтверждения корректировки.
        /// </summary>
        public const string WAITING_CHANGE_PROPERTY = "waiting_change_property";

        /// <summary>
        /// Не использован.
        /// </summary>
        public const string ELIMINATED = "eliminated";
    }
}
