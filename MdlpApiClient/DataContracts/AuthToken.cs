namespace MdlpApiClient.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// MDLP REST API authentication token.
    /// 6.2.2. Метод для получения ключа сессии
    /// </summary>
    [DataContract]
    public class AuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthToken"/>.
        /// </summary>
        public AuthToken()
        {
            // make sure we don't expire prematurely
            CreationDate = DateTime.Now.AddSeconds(-30);
        }

        /// <summary>
        /// Gets the creation date.
        /// </summary>
        [IgnoreDataMember]
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Gets the expiration date.
        /// </summary>
        [IgnoreDataMember]
        public DateTime ExpirationDate
        {
            get { return CreationDate.AddMinutes(LifeTime); }
        }

        /// <summary>
        /// Gets or sets the authentication token.
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the token lifetime, in minutes.
        /// </summary>
        [DataMember(Name = "life_time")]
        public int LifeTime { get; set; }
    }
}
