using TaxCalculator.Models;

namespace TaxCalculator.Interfaces
{
    public interface ITaxCalculator
    {
        public TaxCalculationResult GetTaxCalculationResult(decimal salary);
    }
}
