using Microsoft.AspNetCore.Mvc;
using MultilevelMarketing.Application.Input;
using MultilevelMarketing.Application.Interfaces;
using MultilevelMarketing.Application.Output;

namespace MultilevelMarketing.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusCalculatorController : Controller
    {
        private readonly IBonusCalculatorService _bonusCalculatorService;

        public BonusCalculatorController(IBonusCalculatorService bonusCalculatorService)
        {
            _bonusCalculatorService = bonusCalculatorService;
        }

        [HttpPost]
        public IActionResult Calculate(BonusCalculatorRequest bonusCalculatorView)
        {
            BonusCalculatorResponse response = _bonusCalculatorService.CalculateBonus(bonusCalculatorView);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}