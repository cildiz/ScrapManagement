using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScrapManagement.Application.Features.ActivityLog.Commands.AddLog;
using ScrapManagement.Infrastructure.Identity.Models;
using ScrapManagement.Web.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ScrapManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BasePageModel<LoginModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IMediator _mediator;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Email alanı zorunludur!")]
            [EmailAddress(ErrorMessage = "Geçerli bir email adresi değil!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Şifre alanı zorunludur!")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Beni Hatırla")]
            public bool RememberMe { get; set; }
        }

        public void OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var userCheck = await _userManager.FindByEmailAsync(Input.Email);
                    if (userCheck != null)
                    {
                        userName = userCheck.UserName;
                    }
                }
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        return RedirectToPage("./Deactivated");
                    }
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Başarılı Giriş" });
                            _logger.LogInformation("Kullanıcı giriş yaptı.");
                            _notyf.Success($"{userName} giriş yaptı.");
                            return LocalRedirect(returnUrl);
                        }
                        await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Hatalı Giriş Denemesi" });
                        if (result.IsLockedOut)
                        {
                            _notyf.Warning("Kullanıcı hesabı kilitlendi.");
                            _logger.LogWarning("Kullanıcı hesabı kilitlendi.");
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            _notyf.Error("Geçersiz oturum açma girişimi.");
                            ModelState.AddModelError(string.Empty, "Geçersiz oturum açma girişimi.");
                            return Page();
                        }
                    }
                }
                else
                {
                    _notyf.Error("Giriş bilgileri hatalı, lütfen kontrol edip tekrar deneyiniz.");
                    ModelState.AddModelError(string.Empty, "Giriş bilgileri hatalı, lütfen kontrol edip tekrar deneyiniz.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}