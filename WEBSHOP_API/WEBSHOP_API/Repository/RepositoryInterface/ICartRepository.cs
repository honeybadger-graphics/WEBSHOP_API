﻿using WEBSHOP_API.DTOs;
using WEBSHOP_API.Models;

namespace WEBSHOP_API.Repository.RepositoryInterface
{
    public interface ICartRepository :IDisposable
    {
        Task<Cart> CartDataById(string uId);
        Task CreateCart(Cart cart);
        Task AddToCart(string cartId, AddToCartDTO addToCartDTO);
        Task UpdateCart(Cart cart);
        Task ClearCart(string uId);
        Task DeleteCart(string uId);
        Task<int> GetCartVaule(string uId);
        void Save();
    }
}
