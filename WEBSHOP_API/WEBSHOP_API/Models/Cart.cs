using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class Cart
    {

        [JsonIgnore]
        public int CartId { get; set; }
        public List<Product>? Products { get; set; }
    }
}
