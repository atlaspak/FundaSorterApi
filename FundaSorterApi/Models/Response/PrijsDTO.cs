namespace FundaSorterApi.Models.Response
{
    public class PrijsDTO
    {
        public bool? GeenExtraKosten { get; set; }
        public string? HuurAbbreviation { get; set; }
        public int? Huurprijs { get; set; }
        public string? HuurprijsOpAanvraag { get; set; }
        public int? HuurprijsTot { get; set; }
        public string? KoopAbbreviation { get; set; }
        public int? Koopprijs { get; set; }
        public string? KoopprijsOpAanvraag { get; set; }
        public int? KoopprijsTot { get; set; }
        public int? OriginelePrijs { get; set; }
        public string? VeilingText { get; set; }
    }
}
