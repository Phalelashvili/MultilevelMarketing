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
    public class SaleController : Controller
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public IEnumerable<Sale> GetAll()
        {
            return _saleService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            GeneralResponse response = _saleService.GetById(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpPost]
        public IActionResult Add(AddSaleRequest addSaleView)
        {
            GeneralResponse response = _saleService.Add(addSaleView);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost]
        [Route("filter")]
        public IEnumerable<Sale> Filter(FilterSalesRequest filterSalesRequest)
        {
            return _saleService.Filter(filterSalesRequest);
        }
        
        [HttpPut]
        public IActionResult Update(UpdateSaleRequest updateSaleRequest)
        {
            GeneralResponse response = _saleService.Update(updateSaleRequest);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            GeneralResponse response = _saleService.Delete(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}