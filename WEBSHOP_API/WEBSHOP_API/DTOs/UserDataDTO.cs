using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WEBSHOP_API.Models;
namespace WEBSHOP_API.DTOs
{
    public class UserDataDTO
    {
        
        public string UserId { get; set; }
        public string? UserNameTitles { get; set; }
        
        public string? UserFirstName { get; set; }
       
        public string? UserLastName { get; set; }
       
        public Address UserAddress { get; set; }
        public string? UserLastPurchaseCategory { get; set; }
    }
}
