using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapManagement.Application.Features.ActivityLog.Commands.AddLog;
using ScrapManagement.Application.Interfaces.Shared;
using ScrapManagement.Infrastructure.Identity.Models;
using ScrapManagement.Web.Abstractions;
using System.Threading.Tasks;

namespace ScrapManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : BasePageModel<LogoutModel>
    {
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IAuthenticatedUserService _userService;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IMediator mediator, IAuthenticatedUserService userService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            _userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            await _signInManager.SignOutAsync();

            _logger.LogInformation("Kullanıcı oturumu kapattı.");
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _mediator.Send(new AddActivityLogCommand() { userId = _userService.UserId, Action = "Logged Out" });
            await _signInManager.SignOutAsync();
            _notyf.Information("Kullanıcı oturumu kapattı.");
            _logger.LogInformation("Kullanıcı oturumu kapattı.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}