using Microsoft.AspNetCore.Mvc;

namespace MultilevelMarketing.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}