namespace WEBSHOP_API.Models
{
    public class PostAccuntProductModel
    {//TODO:=> tovabbvinni es ezt hazsnalni... SHEISE
        public Account Account { get; set; }
        public Product Product { get; set; }
    }

    public class AccountEscalatorHelper {
        public Account AccountAdmin { get; set; }
        public Account AccountToEscalate {  get; set; } 
    }
}
