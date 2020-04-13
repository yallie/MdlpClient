namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "timestamp")] // "2020-04-13T12:51:22.873+0000",
        public DateTime TimeStamp { get; set; }

        [DataMember(Name = "status")] // 404,
        public int StatusCode { get; set; }

        [DataMember(Name = "error")] // "Not Found",
        public string Error { get; set; }

        [DataMember(Name = "message")] // "Not Found",
        public string Message{ get; set; }

        [DataMember(Name = "path")] // "/api/v1/reestr/shtuchek/dryuchek"
        public string Path { get; set; }
    }
}
