using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScrapManagement.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class DeactivatedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}