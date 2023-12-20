namespace FundaSorterApi.Models.Response
{
    public class PagingDTO
    {
        public int AantalPaginas { get; set; }
        public int HuidigePagina { get; set; }
        public string? VolgendeUrl { get; set; }
        public string? VorigeUrl { get; set; }
    }
}
