using System.ComponentModel.DataAnnotations.Schema;

namespace WEBSHOP_API.Models
{

    public class Address
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostCode { get; set; }
    }
}
