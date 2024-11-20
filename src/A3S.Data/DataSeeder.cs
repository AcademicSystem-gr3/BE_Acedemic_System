using A3S.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3S.Data
{
    public class DataSeeder
    {
        public async Task SeedAsync(A3SContext context)
        {
            var passwordHasher = new PasswordHasher<User>();

            var rootAdminRoleId = Guid.NewGuid();
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new Role()
                {
                    Id = rootAdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Quản trị viên"
                });
                await context.SaveChangesAsync();
            }
            if (!context.Users.Any())
            {
                var userId = Guid.NewGuid();
                var user = new User()
                {
                    Id = userId,
                    Fullname = "Vu Nguyen",
                    Address ="Ha Noi",
                    Avatar ="abc.png",
                    Email = "vunguyen@gmail.com",
                    NormalizedEmail = "VUNGUYEN@GMAIL.COM",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Status = 1,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LockoutEnabled = false,
                };
                user.PasswordHash = passwordHasher.HashPassword(user, "Admin@123$");
                _ = await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
                {
                    RoleId = rootAdminRoleId,
                    UserId = userId,
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
