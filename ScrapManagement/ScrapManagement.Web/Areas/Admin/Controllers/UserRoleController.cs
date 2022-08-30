using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScrapManagement.Infrastructure.Identity.Models;
using ScrapManagement.Web.Abstractions;
using ScrapManagement.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRoleController : BaseController<UserRoleController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var viewModel = new List<UserRolesViewModel>();
            var user = await _userManager.FindByIdAsync(userId);
            ViewData["Title"] = $"{user.UserName} - Roller";
            ViewData["Caption"] = $"{user.Email}'in rollerini yönetin.";
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                viewModel.Add(userRolesViewModel);
            }
            var model = new ManageUserRolesViewModel()
            {
                UserId = userId,
                UserRoles = viewModel
            };

            return View(model);
        }

        public async Task<IActionResult> Update(string id, ManageUserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            result = await _userManager.AddToRolesAsync(user, model.UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            var currentUser = await _userManager.GetUserAsync(User);
            await _signInManager.RefreshSignInAsync(currentUser);
            await Infrastructure.Identity.Seeds.DefaultAdminUser.SeedAsync(_userManager, _roleManager);
            _notify.Success($"'{user.Email}' kullanıcısı için roller güncellendi.");
            return RedirectToAction("Index", new { userId = id });
        }
    }
}