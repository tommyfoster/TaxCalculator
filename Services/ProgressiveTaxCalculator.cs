using TaxCalculator.Models;
using TaxCalculator.Repositories.Interfaces;
using TaxCalculator.Services.Interfaces;

namespace TaxCalculator.Services
{
    public class ProgressiveTaxCalculator : ITaxCalculator
    {
        private readonly ITaxBandsRepository _taxBandsRepository;

        public ProgressiveTaxCalculator(ITaxBandsRepository taxBandsRepository)
        {
            _taxBandsRepository = taxBandsRepository;
        }

        public List<TaxBand> GetTaxBands(int groupId)
        {
            return _taxBandsRepository.GetTaxBands(groupId);
        }

        public TaxCalculationResult GetTaxCalculationResult(decimal salary)
        {
            if (salary < 0)
            {
                throw new Exception("Error - salary cannot be less than zero!");
            }

            // Get the tax bands associated with this configuration (in this case, group 1)
            var bands = GetTaxBands(1);

            // Start a running total
            decimal total = 0m;

            // Cycle through each band, starting with the lowest
            foreach (var band in bands)
            {
                // In case Upper not specified in band
                decimal upper = band.Upper ?? decimal.MaxValue;
                
                // The amount of salary that is subject to this particular band
                decimal taxable = Math.Max(0, Math.Min(upper, salary) - band.Lower);

                if (taxable > 0)
                {
                    // Tax at the band's rate if any taxable income exists within this band
                    total += taxable * (band.Rate / 100m);
                }

                // If salary does not hit the upper limit, no need to continue
                if (salary <= upper) break;
            }

            // Only returning two values, all others are derived
            return new TaxCalculationResult
            {
                GrossAnnualSalary = salary,
                AnnualTax = total
            };
        }
    }
}
