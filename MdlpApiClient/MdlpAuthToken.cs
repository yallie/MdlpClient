using System;
using System.Runtime.Serialization;

namespace MdlpApiClient
{
    /// <summary>
    /// MDLP REST API authentication token.
    /// </summary>
    [DataContract]
    public class MdlpAuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdlpAuthToken"/>.
        /// </summary>
        public MdlpAuthToken()
        {
            // make sure we don't expire prematurely
            CreationDate = DateTime.Now.AddSeconds(-30);
        }

        [IgnoreDataMember]
        public DateTime CreationDate { get; }

        [IgnoreDataMember]
        public DateTime ExpirationDate => CreationDate.AddMinutes(LifeTime);

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "life_time")]
        public int LifeTime { get; set; }
    }
}
