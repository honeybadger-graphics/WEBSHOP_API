using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Repository.RepositoryInterface
{

    public interface IProductRepository : IDisposable
    {
        Task<IEnumerable<Product>> GetProducts(int page, int numberOFProductsToDispaly);
        Task<IEnumerable<Product>> GetProductsByCategory(string category, int page, int numberOFProductsToDispaly);
        Task<IEnumerable<Product>> GetProductsIfOnSale(int page, int numberOFProductsToDispaly);
        Task<IEnumerable<Product>> GetProductsIfPromoted(int page, int numberOFProductsToDispaly);
        Task<Product> GetProductById(int pruductId);
        Task<Product> GetProductByName(string productName);
        Task<Product> GetProductByProduct(Product product);
        Task<Product> AddProduct(Product product);
        Task CreateProductAndStock(Product product);
        Task UpdateProduct(Product productToUpdate);
        Task DeleteProduct(int productId);
        void Save();

    }

}
