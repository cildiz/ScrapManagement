using Microsoft.AspNetCore.Mvc;
using ScrapManagement.Web.Abstractions;

namespace ScrapManagement.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class HomeController : BaseController<HomeController>
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}