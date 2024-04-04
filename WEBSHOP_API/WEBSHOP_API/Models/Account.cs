using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace WEBSHOP_API.Models
{
    public class Account
    {
        [JsonIgnore]
        public int AccountId { get; set; }
        [JsonIgnore]
        public string? AccountName { get; set; }
       
        public string? AccountPassword { get; set; }
        public string? AccountEmail { get; set; }
        [JsonIgnore]
        public bool IsAdmin { get; set; } = false;
        [JsonIgnore]
        public Cart? Cart { get; set; }
    }
}
