using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class Product
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public List<string>? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductImage { get; set; } // URL for image
        public int ProductCount { get; set; }
        [JsonIgnore]
        public int ProductStock { get; set; }
       
        public int ProductPrice { get; set; }
        [JsonIgnore]
        public int ProductBasePrice { get; set; }
        [JsonIgnore]
        public bool IsProductPromoted { get; set; }
        [JsonIgnore]
        public bool IsProductOnSale { get; set; }
    }
    
}
