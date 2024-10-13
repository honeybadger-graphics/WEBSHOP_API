using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.OpenApi.Models;
using ServiceStack;
using System.Security.Claims;
using WEBSHOP_API;
using WEBSHOP_API.Controllers;
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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.BearerScheme;
    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
}).AddBearerToken(IdentityConstants.BearerScheme);
//builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme).AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<UserDbContext>().AddApiEndpoints();
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigration();
}
app.UseHttpsRedirection();
app.MapControllers();
app.MapGroup("/api/User").MapIdentityApi<User>();
using (var scope = app.Services.CreateScope()) {
 var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] {"Admin"/*,"Customer"*/ };
    foreach (var role in roles) {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
//Keep it commented for now
/*using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "email";
    string password = "password";
    if (await userManager.FindByEmailAsync(email) == null) {
        var user = new User();
        user.Email = email;
        user.UserName = email;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user,"Admin");
        //await userManager.AddToRoleAsync(user, "Customer");
    }
}*/
app.MapGroup("/api/User").MapGet("/me", async (ClaimsPrincipal claims, UserDbContext context)=> {
    string uId = claims.Claims.First(c => c.Type ==ClaimTypes.NameIdentifier).Value;
    return await context.Users.FindAsync(uId);
}).RequireAuthorization();
app.Run();
