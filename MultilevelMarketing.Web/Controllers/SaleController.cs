using Microsoft.AspNetCore.Mvc;
using MultilevelMarketing.Application.Interfaces;

namespace MultilevelMarketing.Web.Controllers
{
    [Route("[controller]s")]
    public class SaleController : Controller
    {
        private ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpGet]
        [Route("update/{id:int}")]
        public IActionResult Update(int id)
        {
            var serviceResponse = _saleService.GetById(id);
            if (!serviceResponse.Success)
                return RedirectToAction("Index", "Sale");

            ViewBag.Model = serviceResponse.Sale;
            return View();
        }
        
        [HttpGet]
        [Route("filter")]
        public IActionResult Filter()
        {
            return View();
        }
    }
}