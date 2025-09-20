using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Controllers
{
    [ApiController]
    [Route("api")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxController(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        [HttpGet("calculate")]
        public ActionResult<TaxCalculationResult> Calculate([FromQuery] decimal gross)
        {
            if (gross < 0)
                return BadRequest("Gross salary must be non-negative.");

            var result = _taxCalculator.GetTaxCalculationResult(gross);
            return Ok(result);
        }
    }
}

