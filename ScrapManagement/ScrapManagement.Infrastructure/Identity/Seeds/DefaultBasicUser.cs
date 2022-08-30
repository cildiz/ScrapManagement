using Microsoft.AspNetCore.Identity;
using ScrapManagement.Application.Enums;
using ScrapManagement.Infrastructure.Identity.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "user",
                Email = "user@user.com",
                FirstName = "TestAd",
                LastName = "TestSoyad",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "User.12345!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                }
            }
        }
    }
}