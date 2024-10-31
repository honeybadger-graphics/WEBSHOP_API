using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WEBSHOP_API.Models
{
    public class UserData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        
        public string? UserNameTitles { get; set; }
        
        public string? UserFirstName { get; set; }
       
        public string? UserLastName { get; set; }
       
        public Address? UserAddress { get; set; }
        public string? UserLastPurchaseCategory { get; set; }

    }
   
}
