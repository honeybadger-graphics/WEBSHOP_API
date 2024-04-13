namespace WEBSHOP_API.Models
{
    public class Stocks
    {
        public int Id { get; set; }
        public int ProductStocks { get; set; } = 0;
        public Product Product { get; set; }
    }
}
