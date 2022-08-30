using Microsoft.AspNetCore.Identity;
using ScrapManagement.Application.Enums;
using ScrapManagement.Infrastructure.Identity.Models;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        }
    }
}