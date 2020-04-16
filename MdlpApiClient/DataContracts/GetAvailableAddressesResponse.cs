namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.2.5. Метод получения информации об адресах искомого участника.
    /// </summary>
    [DataContract]
    public class GetAvailableAddressesResponse
    {
        [DataMember(Name = "entries")]
        public RegistrationAddress[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
