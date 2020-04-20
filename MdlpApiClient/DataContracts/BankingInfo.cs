namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.9.1. Метод для получения информации об организации, в которой зарегистрирован текущий пользователь
    /// Формат объекта BankingInfo
    /// </summary>
    [DataContract]
    public class BankingInfo
    {
        /// <summary>
        /// Номер расчетного счета
        /// </summary>
        [DataMember(Name = "checking_account", IsRequired = false)]
        public string Account { get; set; }

        /// <summary>
        /// Наименование банка
        /// </summary>
        [DataMember(Name = "bank", IsRequired = false)]
        public string Bank { get; set; }

        /// <summary>
        /// Номер корреспондентского счета
        /// </summary>
        [DataMember(Name = "correspondent_account", IsRequired = false)]
        public string CorrespondentAccount { get; set; }

        /// <summary>
        /// Основание для действий руководителя
        /// 1 — доверенность
        /// 2 — учредительные документы
        /// </summary>
        [DataMember(Name = "authorized_by", IsRequired = false)]
        public int AuthorizedBy { get; set; }

        /// <summary>
        /// БИК банка
        /// </summary>
        [DataMember(Name = "bic", IsRequired = false)]
        public string Bic { get; set; }

        /// <summary>
        /// Подписант
        /// </summary>
        [DataMember(Name = "signer", IsRequired = false)]
        public string Signer { get; set; }
    }
}
