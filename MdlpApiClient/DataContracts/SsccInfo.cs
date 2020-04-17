namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.4.1. Информация об иерархии вложенности третичной упаковки
    /// </summary>
    [DataContract]
    public class SsccInfo
    {
        /// <summary>
        /// Идентификационный код третичной упаковки
        /// </summary>
        [DataMember(Name = "sscc")]
        public string Sscc { get; set; }

        /// <summary>
        /// Дата и время совершения операции упаковки
        /// </summary>
        [DataMember(Name = "release_date")]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Идентификатор субъекта обращения, осуществившего операцию упаковки
        /// </summary>
        /// <remarks>
        /// Идентификационный код SysID или BranchID
        /// </remarks>
        [DataMember(Name = "system_subj_id")]
        public string SystemSubjectID { get; set; }
    }
}
