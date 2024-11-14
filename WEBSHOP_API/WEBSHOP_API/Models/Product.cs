namespace WEBSHOP_API.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public List<string> ProductDescription { get; set; } = [];
        public string? ProductCategory { get; set; }
        public string? ProductImage { get; set; } // URL for image
       
       
        public int ProductPrice { get; set; }
      
        public int ProductBasePrice { get; set; }

        public bool IsProductPromoted { get; set; } = false;

        public bool IsProductOnSale { get; set; } = false;

    }
    
}
