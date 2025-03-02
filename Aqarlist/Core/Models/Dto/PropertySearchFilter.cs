namespace Aqarlist.Core.Models.Dto
{
    public class PropertySearchFilter
    {
        public string? SearchTerm { get; set; }
        public int? MinPriceFilter { get; set; }
        public int? MaxPriceFilter { get; set; }
        public string? CityFilter { get; set; }
        public int? MinSizeFilter { get; set; }
        public int? MaxSizeFilter { get; set; }
    }
}
