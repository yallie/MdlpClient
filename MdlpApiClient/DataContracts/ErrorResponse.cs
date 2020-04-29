namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Структура, которая содержит поля, встречающиеся в сообщениях об ошибках.
    /// </summary>
    [DataContract]
    public class ErrorResponse
    {
        // Sometimes error response has this structure: { timestamp, status, error, message, path }

        /// <summary>
        /// Время ошибки
        /// </summary>
        [DataMember(Name = "timestamp")] // "2020-04-13T12:51:22.873+0000",
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// HTTP-статус ошибки, числовой код
        /// </summary>
        [DataMember(Name = "status")] // 404,
        public int StatusCode { get; set; }

        /// <summary>
        /// HTTP-статус ошибки, текстовое представление
        /// </summary>
        [DataMember(Name = "error")] // "Not Found",
        public string Error { get; set; }

        /// <summary>
        /// Сообщение об ошибке, текстовое представление
        /// </summary>
        [DataMember(Name = "message")] // "Not Found",
        public string Message{ get; set; }

        /// <summary>
        /// Запрошенный путь
        /// </summary>
        [DataMember(Name = "path")] // "/api/v1/reestr/shtuchek/dryuchek"
        public string Path { get; set; }

        // And sometimes it's like { error_description: "hey" }

        /// <summary>
        /// Сообщение об ошибке, текстовое представление
        /// </summary>
        [DataMember(Name = "error_description")] // "Ошибка такая-то с подробностями",
        public string Description { get; set; }

        // And sometimes it's like { success: false, violations: ["one", "two", "three"] }

        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "violations")]
        public string[] Violations { get; set; }
    }
}
