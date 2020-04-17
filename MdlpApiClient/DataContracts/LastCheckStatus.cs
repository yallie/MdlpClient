namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.3.6. Метод для поиска по реестру КИЗ записей, ожидающих
    /// вывода из оборота по чеку от контрольно-кассовой техники (ККТ).
    /// Статус последней проверки.
    /// </summary>
    [DataContract]
    public class LastCheckStatus
    {
        /// <summary>
        /// Время последней проверки
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Список нарушений при попытке обработки чека
        /// 1 — нарушение лицензионных требований
        /// 2 — повторный вывод из оборота
        /// 3 — отсутствуют сведения о вводе в оборот
        /// 4 — не подлежит розничной реализации
        /// 5 — нарушение формата чека
        /// 6 — нарушение порядка предоставления сведений
        /// 7 — нарушение правовладения
        /// 8 — истек срок годности
        /// 9 — отсутствие информации о рецепте
        /// </summary>
        [DataMember(Name = "violation_reasons")]
        public int[] ViolationReasons { get; set; }
    }
}
