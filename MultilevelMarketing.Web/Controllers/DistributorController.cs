using Microsoft.AspNetCore.Mvc;
using MultilevelMarketing.Application.Interfaces;

namespace MultilevelMarketing.Web.Controllers
{
    [Route("[controller]s")]
    public class DistributorController : Controller
    {
        private IDistributorService _distributorService;

        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("register")]
        [Route("add")]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpGet]
        [Route("update/{id:int}")]
        public IActionResult Update(int id)
        {
            var serviceResponse = _distributorService.GetById(id);
            if (!serviceResponse.Success)
                return RedirectToAction("Index", "Distributor");

            ViewBag.Model = serviceResponse.Distributor;
            return View();
        }
    }
}