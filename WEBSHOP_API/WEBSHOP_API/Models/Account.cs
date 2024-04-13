using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class Account
    {
        [JsonIgnore]
        public int AccountId { get; set; }
        
        public string? AccountNameTitles { get; set; }
        
        public string? AccountFirstName { get; set; }
       
        public string? AccountLastName { get; set; }
        public string? AccountPassword { get; set; }
        public string? AccountEmail { get; set; }
       
        public string? AccountAddress { get; set; }
        [JsonIgnore]
        public bool IsAdmin { get; set; } = false;
        public List<Product>? Cart { get; set; }
    }
}
