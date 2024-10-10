using Microsoft.EntityFrameworkCore;
using WEBSHOP_API.Database;

namespace WEBSHOP_API.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using UserDbContext context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            context.Database.Migrate();
        }
    }
}
