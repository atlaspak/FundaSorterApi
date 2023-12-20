namespace FundaSorterApi.Models.Response
{
    public class KoopResponseDTO
    {
        public int? AccountStatus { get; set; }
        public bool? EmailNotConfirmed { get; set; }
        public bool? ValidationFailed { get; set; }
        public dynamic? ValidationReport { get; set; }
        public int? Website { get; set; }
        public MetaDataDTO? Metadata { get; set; }
        public List<RealEstateDTO>? Objects { get; set; }
        public PagingDTO? Paging { get; set; }
    }
}
