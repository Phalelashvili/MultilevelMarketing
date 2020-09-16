using Microsoft.AspNetCore.Mvc;
using MultilevelMarketing.Application.Interfaces;

namespace MultilevelMarketing.Web.Controllers
{
    [Route("[controller]s")]
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
            var serviceResponse = _productService.GetById(id);
            if (!serviceResponse.Success)
                return RedirectToAction("Index", "Product");

            ViewBag.Model = serviceResponse.Product;
            return View();
        }
    }
}