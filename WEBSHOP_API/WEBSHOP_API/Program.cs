using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using WEBSHOP_API.Models;
using WEBSHOP_API.Repository;
using WEBSHOP_API.Repository.RepositoryInterface;
using WEBSHOP_API.Storage;
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<WebshopDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
