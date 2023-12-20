namespace FundaSorterApi.Models.Response
{
    public class PromoLabelDTO
    {
        public bool? HasPromotionLabel { get; set; }
        List<string?>? PromotionPhotos { get; set; }
        List<string?>? PromotionPhotosSecure { get; set; }
        public int? PromotionType { get; set; }
        public int? RibbonColor { get; set; }
        public string? RibbonText { get; set; }
        public string? Tagline { get; set; }
    }
}
