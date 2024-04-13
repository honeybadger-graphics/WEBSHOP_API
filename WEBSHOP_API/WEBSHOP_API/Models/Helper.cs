namespace WEBSHOP_API.Models
{
    public class PostAccuntProductModel
    { // probably not needed anymore should remove this.
        public Account Account { get; set; }
        public Product Product { get; set; }
    }

    public class AccountEscalatorHelper {
        public LoginCreds Admin { get; set; }
        public int AccountToEscalateId {  get; set; } 
    }
    public class LoginCreds
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
    public class ProductDTO
    {
        public string? ProductDTOName { get; set; }
        public List<string>? ProductDTODescription { get; set; }
        public string? ProductDTOCategory { get; set; }
        public string? ProductDTOImage { get; set; } // URL for image
        public int? ProductDTOCount { get; set; }
        public int ProductDTOPrice { get; set; }

        public int ProductDTOBasePrice { get; set; }
    }
}
