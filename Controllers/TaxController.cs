using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    [ApiController]
    [Route("api")]
    public class TaxController : ControllerBase
    {
        private readonly ProgressiveTaxCalculator _calculator;

        public TaxController()
        {
            // In this scenario, I'm just taking the tax bands given in the assignment
            // We could retrieve these from a DB by injecting a repository
            var bands = new List<TaxBand>
            {
                new TaxBand(0, 5000, 0),
                new TaxBand(5000, 20000, 20),
                new TaxBand(20000, null, 40)
            };

            _calculator = new ProgressiveTaxCalculator(bands);
        }

        [HttpGet("calculate")]
        public ActionResult<TaxCalculationResult> Calculate([FromQuery] decimal gross)
        {
            if (gross < 0)
                return BadRequest("Gross salary must be non-negative.");

            var result = _calculator.GetTaxCalculationResult(gross);
            return Ok(result);
        }
    }
}

