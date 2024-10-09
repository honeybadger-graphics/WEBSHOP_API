using System.Text.Json.Serialization;

namespace WEBSHOP_API.DTOs
{
    public class CartDTO
    {
        [JsonIgnore]
        public int CartId { get; set; }
        public List<int>? ProductsId { get; set; }
        public List<int>? ProductsCounts { get; set; }
    }

}
