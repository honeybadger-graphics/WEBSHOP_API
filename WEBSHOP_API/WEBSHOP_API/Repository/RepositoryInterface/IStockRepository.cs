using WEBSHOP_API.Models;

namespace WEBSHOP_API.Repository.RepositoryInterface
{
    public interface IStockRepository : IDisposable
    {
        Task<Stock> GetStockByProductId(int productId);
        Task UpdateStock(int productId, int stockChange);
        Task DeleteStock(int productId);
        void Save();
    }
}
