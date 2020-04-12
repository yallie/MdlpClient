namespace MdlpApiClient
{
    using System;
    using System.Net;
    using System.Runtime.Serialization;

    [Serializable]
    public class MdlpException : Exception
    {
        public MdlpException(HttpStatusCode code, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = code;
        }

        protected MdlpException(SerializationInfo info, StreamingContext context)
        {
            StatusCode = (HttpStatusCode)info.GetInt32("Code");
        }

        public HttpStatusCode StatusCode { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Code", (int)StatusCode);
        }
    }
}
