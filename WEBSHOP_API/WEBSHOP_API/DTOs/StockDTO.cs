﻿namespace WEBSHOP_API.DTOs
{
    public class StockDTO
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int ProductStocks { get; set; } = 0;
       
    }
}
