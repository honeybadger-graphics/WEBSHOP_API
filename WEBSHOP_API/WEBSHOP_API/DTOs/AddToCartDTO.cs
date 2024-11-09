namespace WEBSHOP_API.DTOs
{
    public class AddToCartDTO
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
    }
}
