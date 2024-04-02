namespace WEBSHOP_API.Storage
{
    public class StorageLogger
    {
        public int StorageLoggerId { get; set; }
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int StockChange { get; set; }
        public string? Reason { get; set; }
        
    }
}
