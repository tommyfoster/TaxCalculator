namespace TaxCalculator.Models
{
    public class TaxBand
    {
        public int Lower { get; }
        public int? Upper { get; }
        public int Rate { get; }

        public TaxBand(int lower, int? upper, int rate)
        {
            Lower = lower;
            Upper = upper;
            Rate = rate;
        }
    }
}
