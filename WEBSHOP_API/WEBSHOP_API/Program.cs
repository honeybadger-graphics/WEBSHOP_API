using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WEBSHOP_API;
using WEBSHOP_API.Database;
using WEBSHOP_API.Extensions;
using WEBSHOP_API.Repository;
using WEBSHOP_API.Repository.RepositoryInterface;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<WebshopDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("ProductDBConnection")));
builder.Services.AddDbContext<UserDbContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("UserDBConnection")));
         
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
          //  options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddCookie(IdentityConstants.ApplicationScheme).AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<UserDbContext>().AddApiEndpoints();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigration();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapGroup("/api/User").MapIdentityApi<User>();

app.Run();
