namespace MdlpApiClient.DataContracts
{
    using System.Runtime.Serialization;

    /// <summary>
    /// 8.1.2. Список мест осуществления деятельности.
    /// </summary>
    [DataContract]
    public class GetBranchesResponse
    {
        [DataMember(Name = "entries")]
        public BranchEntry[] Entries { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
