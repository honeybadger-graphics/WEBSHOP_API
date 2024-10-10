using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WEBSHOP_API.Database
{
    public class UserDbContext: IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().Property(u => u.UserNameTitles).HasMaxLength(5);
            //builder.HasDefaultSchema("identity");
        }
    }
}
