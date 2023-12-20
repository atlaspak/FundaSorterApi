namespace FundaSorterApi.Models.Response
{
    public class ProjectDTO
    {
        public string? AantalKamersTotEnMet { get; set; }
        public string? AantalKamersVan { get; set; }
        public string? AantalKavels { get; set; }
        public string? Adres { get; set; }
        public string? FriendlyUrl { get; set; }
        public string? GewijzigdDatum { get; set; }
        public string? HoofdFoto { get; set; }
        public bool? IndIpix { get; set; }
        public bool? IndPDF { get; set; }
        public bool? IndPlattegrond { get; set; }
        public bool? IndTop { get; set; }
        public bool? IndVideo { get; set; }
        public string? InternalId { get; set; }
        public string? MaxWoonoppervlakte { get; set; }
        public string? MinWoonoppervlakte { get; set; }
        public string? Naam { get; set; }
        public string? Omschrijving { get; set; }
        List<string?>? OpenHuizen { get; set; }
        public string? Plaats { get; set; }
        public int? Prijs { get; set; }
        public string? PrijsGeformatteerd { get; set; }
        public string? PublicatieDatum { get; set; }
        public int? Type { get; set; }
        public string? Woningtypen { get; set; }
    }
}
