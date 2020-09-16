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
    public class DistributorController : Controller
    {
        private readonly IDistributorService _distributorService;

        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }
        
        [HttpGet]
        public IEnumerable<Distributor> GetAll()
        {
            return _distributorService.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            GeneralResponse response = _distributorService.GetById(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
        
        [HttpPost]
        public IActionResult Register(RegisterDistributorRequest registerDistributor)
        {
            GeneralResponse response = _distributorService.Register(registerDistributor);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        public IActionResult Update(UpdateDistributorRequest updateDistributorRequest)
        {
            GeneralResponse response = _distributorService.Update(updateDistributorRequest);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);

        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            GeneralResponse response = _distributorService.Delete(id);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);

        }
    }
}