using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScrapManagement.Application.Constants;
using ScrapManagement.Infrastructure.Identity.Models;
using ScrapManagement.Web.Abstractions;
using ScrapManagement.Web.Areas.Admin.Models;
using ScrapManagement.Web.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrapManagement.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionController : BaseController<PermissionController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index(string roleId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.ScrapTypes), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Dashboard), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Scraps), roleId);
            allPermissions.GetPermissions(typeof(Permissions.Users), roleId);
            var role = await _roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await _roleManager.GetClaimsAsync(role);
            var claimsModel = _mapper.Map<List<RoleClaimsViewModel>>(claims);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claimsModel.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = _mapper.Map<List<RoleClaimsViewModel>>(allPermissions);
            ViewData["Title"] = $"{role.Name} Rolü İçin İzinler";
            ViewData["Caption"] = $"{role.Name} Rol İzinlerini Yönetin";
            _notify.Success($"'{role.Name}' Rolü İçin Güncellenmiş Haklar/İzinler");
            return View(model);
        }

        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            //Remove all Claims First
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddPermissionClaim(role, claim.Value);
            }
            _notify.Success($"'{role.Name}' Rolü İçin Güncellenmiş Haklar/İzinler");
            //var user = await _userManager.GetUserAsync(User);
            //await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index", new { roleId = model.RoleId });
        }
    }
}