namespace WEBSHOP_API.Models
{
    public class StorageLogger
    {


        public StorageLogger(int accountId, int productId, int stockChange, string? reason)
        {
            Date = DateTime.Now.ToLongTimeString();
            AccountId = accountId;
            ProductId = productId;
            StockChange = stockChange;
            Reason = reason;
        }
        public string Date { get; set; }
        public int StorageLoggerId { get; set; }
        public int AccountId { get; set; }
        public int ProductId { get; set; }
        public int StockChange { get; set; }
        public string? Reason { get; set; }

    }
}
