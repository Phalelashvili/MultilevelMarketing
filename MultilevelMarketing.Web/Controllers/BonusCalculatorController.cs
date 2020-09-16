using Microsoft.AspNetCore.Mvc;

namespace MultilevelMarketing.Web.Controllers
{
    public class BonusCalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}