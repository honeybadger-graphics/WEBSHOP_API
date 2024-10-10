using Microsoft.AspNetCore.Identity;

namespace WEBSHOP_API.Database
{
    public class User: IdentityUser
    {
        public string? UserNameTitles { get; set; }

        public string? UserFirstName { get; set; }

        public string? UserLastName { get; set; }
        
        public string? UserAddress { get; set; }
    }
}
