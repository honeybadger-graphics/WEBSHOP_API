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
        Product GetProductById(int pruductId);
        Product GetProductByName(string productName);
        Product GetProductByProduct(Product product);
        void CreateProductAndStock(Product product);
        void UpdateProduct(Product productToUpdate);
        void DeleteProduct(int productId);
        void Save();

    }

}
