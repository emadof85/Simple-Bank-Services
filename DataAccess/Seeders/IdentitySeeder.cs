using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seeders
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndUsers(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Accountant" };
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync( new IdentityRole(role));
                }
            }
            var adminEmail = "admin@admin.com";
            var _user = await userManager.FindByEmailAsync(adminEmail);
            if (_user == null)
            {
                _user = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Admin",
                    Address = "Any Address",
                    CreatedAt = DateTime.Now,
                };

                var result = await userManager.CreateAsync(_user,"Admin123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(_user, "Admin");
            }
        }
    }
}
