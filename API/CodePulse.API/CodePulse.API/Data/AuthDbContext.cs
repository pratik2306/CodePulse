using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Create Reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = "c7bd7324-09ed-43b4-8dc4-0dc1a0e2d63b",
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = "c7bd7324-09ed-43b4-8dc4-0dc1a0e2d63b"

                },
                new IdentityRole()
                {
                    Id = "d63f81e9-9738-4119-9f69-041265b200b1",
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = "d63f81e9-9738-4119-9f69-041265b200b1"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Create Admin User

            var user = new IdentityUser()
            {
                Id = "eed3bba5-58a0-42ed-872c-4c0a2381eed9",
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper(),
            };

            user.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(user, "Admin@123");
            builder.Entity<IdentityUser>().HasData(user);

            //Give Roles to Admin

            var userRole = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = "eed3bba5-58a0-42ed-872c-4c0a2381eed9",
                    RoleId = "c7bd7324-09ed-43b4-8dc4-0dc1a0e2d63b"
                },
                new()
                {
                    UserId = "eed3bba5-58a0-42ed-872c-4c0a2381eed9",
                    RoleId = "d63f81e9-9738-4119-9f69-041265b200b1"
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userRole);
        }
    }
}
