namespace TaxCalculator.Models
{
    public class TaxCalculationResult
    {
        public decimal GrossAnnualSalary { get; set; }
        public decimal GrossMonthlySalary => GrossAnnualSalary / 12m;
        public decimal AnnualTax { get; set; }
        public decimal MonthlyTax => AnnualTax / 12m;
        public decimal NetAnnualSalary => GrossAnnualSalary - AnnualTax;
        public decimal NetMonthlySalary => NetAnnualSalary / 12m;
    }
}
