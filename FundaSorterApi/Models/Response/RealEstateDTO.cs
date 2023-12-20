using System.Text.Json.Serialization;

namespace FundaSorterApi.Models.Response
{
    public class RealEstateDTO
    {
        public string? AangebodenSindsTekst { get; set; }
        public string? AanmeldDatum { get; set; }
        public int? AantalBeschikbaar { get; set; }
        public int? AantalKamers { get; set; }
        public int? AantalKavels { get; set; }
        public string? Aanvaarding { get; set; }
        public string? Adres { get; set; }
        public int? Afstand { get; set; }
        public string? BronCode { get; set; }
        public List<dynamic>? ChildrenObjects { get; set; }
        public string? DatumAanvaarding { get; set; }
        public string? DatumOndertekeningAkte { get; set; }
        public string? Foto { get; set; }
        public string? FotoLarge { get; set; }
        public string? FotoLargest { get; set; }
        public string? FotoMedium { get; set; }
        public string? FotoSecure { get; set; }
        public string? GewijzigdDatum { get; set; }
        public int? GlobalId { get; set; }
        public string? GroupByObjectType { get; set; }
        public bool? Heeft360GradenFoto { get; set; }
        public bool? HeeftBrochure { get; set; }
        public bool? HeeftOpenhuizenTopper { get; set; }
        public bool? HeeftOverbruggingsgrarantie { get; set; }
        public bool? HeeftPlattegrond { get; set; }
        public bool? HeeftTophuis { get; set; }
        public bool? HeeftVeiling { get; set; }
        public bool? HeeftVideo { get; set; }
        public int? HuurPrijsTot { get; set; }
        public int? Huurprijs { get; set; }
        public string? HuurprijsFormaat { get; set; }
        public string? Id { get; set; }
        public string? InUnitsVanaf { get; set; }
        public bool? IndProjectObjectType { get; set; }
        public string? IndTransactieMakelaarTonen { get; set; }
        public bool? IsSearchable { get; set; }
        public bool? IsVerhuurd { get; set; }
        public bool? IsVerkocht { get; set; }
        public bool? IsVerkochtOfVerhuurd { get; set; }
        public int? Koopprijs { get; set; }
        public string? KoopprijsFormaat { get; set; }
        public int? KoopprijsTot { get; set; }
        public string? Land { get; set; }
        public int? MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
        public string? MobileURL { get; set; }
        public string? Note { get; set; }
        public List<string?>? OpenHuis { get; set; }
        public int? Oppervlakte { get; set; }
        public int? Perceeloppervlakte { get; set; }
        public string? Postcode { get; set; }
        public PrijsDTO? Prijs { get; set; }
        public string? PrijsGeformatteerdHtml { get; set; }
        public string? PrijsGeformatteerdTextHuur { get; set; }
        public string? PrijsGeformatteerdTextKoop { get; set; }
        public List<string?>? Producten { get; set; }
        public ProjectDTO? Project { get; set; }
        public string? ProjectNaam { get; set; }
        public PromoLabelDTO? PromoLabel { get; set; }
        public string? PublicatieDatum { get; set; }
        public int? PublicatieStatus { get; set; }
        public string? SavedDate { get; set; }

        [JsonPropertyName("Soort-aanbod")]
        public string Soort_aanbod { get; set; }
        public int? SoortAanbod { get; set; }
        public string? StartOplevering { get; set; }
        public string? TimeAgoText { get; set; }
        public string? TransactieAfmeldDatum { get; set; }
        public string? TransactieMakelaarId { get; set; }
        public string? TransactieMakelaarNaam { get; set; }
        public int? TypeProject { get; set; }
        public string? URL { get; set; }
        public string? VerkoopStatus { get; set; }
        public double? WGS84_X { get; set; }
        public double? WGS84_Y { get; set; }
        public int? WoonOppervlakteTot { get; set; }
        public int? Woonoppervlakte { get; set; }
        public string? Woonplaats { get; set; }
        public List<int>? ZoekType { get; set; }
    }
}
