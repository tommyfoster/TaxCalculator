using TaxCalculator.Models;

namespace TaxCalculator.Repositories.Interfaces
{
    public interface ITaxBandsRepository
    {
        public List<TaxBand> GetTaxBands(int groupId);
    }
}
