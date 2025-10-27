namespace CarManagementAPI.Models
{
    public class CarFilter
    {
        public int? DealerId { get; set;}
        public string? DealerName { get; set;}
        public bool? IsAvailable { get; set; }
        public string Brand { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public string SearchModel { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
