namespace TaxCalculator.Models
{
    public class TaxBand
    {
        public decimal Lower { get; }
        public decimal? Upper { get; }
        public int Rate { get; }

        public TaxBand(decimal lower, decimal? upper, int rate)
        {
            Lower = lower;
            Upper = upper;
            Rate = rate;
        }
    }
}
