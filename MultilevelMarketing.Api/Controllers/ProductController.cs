using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Input.UpdateRequests;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _productService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            GeneralResponse response = _productService.GetById(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpPost]
        public IActionResult Add(AddProductRequest addProductView)
        {
            GeneralResponse response = _productService.Add(addProductView);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpPut]
        public IActionResult Update(UpdateProductRequest updateProductView)
        {
            GeneralResponse response = _productService.Update(updateProductView);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            GeneralResponse response = _productService.Delete(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}