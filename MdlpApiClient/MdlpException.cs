namespace MdlpApiClient
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;
    using MdlpApiClient.DataContracts;

    [Serializable]
    public class MdlpException : Exception
    {
        public MdlpException(HttpStatusCode code, string message, ErrorResponse errorResponse, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = code;
            ErrorResponse = errorResponse;
        }

        protected MdlpException(SerializationInfo info, StreamingContext context)
        {
            StatusCode = (HttpStatusCode)info.GetInt32("Code");
            if (info.GetString("Path") != null)
            {
                ErrorResponse = new ErrorResponse
                {
                    TimeStamp = info.GetDateTime("TimeStamp"),
                    StatusCode = info.GetInt32("StatusCode"),
                    Error = info.GetString("Error"),
                    Message = info.GetString("Message"),
                    Path = info.GetString("Path"),
                };
            }
        }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorResponse ErrorResponse { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Code", (int)StatusCode);
            if (ErrorResponse != null)
            {
                info.AddValue("TimeStamp", ErrorResponse.TimeStamp);
                info.AddValue("StatusCode", ErrorResponse.StatusCode);
                info.AddValue("Error", ErrorResponse.Error);
                info.AddValue("Message", ErrorResponse.Message);
                info.AddValue("Path", ErrorResponse.Path);
            }
        }
    }
}
