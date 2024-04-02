using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class Product
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string[]? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductImage { get; set; } // URL for image
        public int ProductCount { get; set; }
        [JsonIgnore]
        public int ProductStock { get; set; }
        [JsonIgnore]
        public int ProductPrice { get; set; }
        public int ProductBasePrice { get; set; }
        public bool IsProductPromoted { get; set; }
        public bool IsProductOnSale { get; set; }
    }
    public class PostAccuntProductModel
    {//TODO:=> tovabbvinni es ezt hazsnalni... SHEISE
        public Account account { get; set; }
        public Product product { get; set; }
    }
}
