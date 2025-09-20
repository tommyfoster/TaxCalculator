using TaxCalculator.Models;

namespace TaxCalculator.Services.Interfaces
{
    public interface ITaxCalculator
    {
        List<TaxBand> GetTaxBands(int groupId);

        public TaxCalculationResult GetTaxCalculationResult(decimal salary);
    }
}
