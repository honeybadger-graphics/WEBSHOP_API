using System.Text.Json.Serialization;

namespace WEBSHOP_API.DTOs
{
    public class CartDTO
    {
        public string CartId { get; set; }
        public List<int> ProductIds { get; set; } = [];
        public List<int> ProductCount { get; set; } = [];
    }

}
