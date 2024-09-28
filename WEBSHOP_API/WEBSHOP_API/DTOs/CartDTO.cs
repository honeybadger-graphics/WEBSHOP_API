﻿using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class CartDTO
    {
        [JsonIgnore]
        public int CartId { get; set; }
        public List<string>? ProductsName { get; set; }
        public List<int>? ProductsCounts { get; set; }
    }

}
