using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaxCalculator.Models;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;

namespace TaxCalcuator.Tests
{
    [TestClass]
    public class ProgressiveTaxCalculatorTests
    {
        private readonly ITaxCalculator _taxCalculator;

        public ProgressiveTaxCalculatorTests(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }
       
        [DataTestMethod]
        [DataRow(-1, 0)]       // Negative value
        [DataRow(0, 0)]        // No income
        [DataRow(4000, 0)]     // Below first band
        [DataRow(10000, 1000)] // 5000 at 0%, 5000 at 20% = 1000
        [DataRow(25000, 5000)] // 15000 at 20% + 5000 at 40% = 5000
        [DataRow(40000, 11000)]// Example from spec
        public void Calculate_CorrectTax(int gross, int expectedTax)
        {
            try
            {
                var result = _taxCalculator.GetTaxCalculationResult(gross);
                Assert.AreEqual(expectedTax, result.AnnualTax);
            }
            catch (Exception e)
            {
                Assert.AreEqual("Error - salary cannot be less than zero!", e.Message);
            }           
        }

        [TestMethod]
        public void NetSalary_IsGrossMinusTax()
        {
            var result = _taxCalculator.GetTaxCalculationResult(40000);

            Assert.AreEqual(Math.Round(result.GrossAnnualSalary - result.AnnualTax, 2), Math.Round(result.NetAnnualSalary, 2));
            Assert.AreEqual(Math.Round(result.GrossMonthlySalary - result.MonthlyTax, 2), Math.Round(result.NetMonthlySalary, 2));
        }

        [TestMethod]
        public void MonthlyValues_AreAnnualDividedBy12()
        {
            var result = _taxCalculator.GetTaxCalculationResult(12000);

            Assert.AreEqual(Math.Round(result.GrossAnnualSalary / 12, 2), Math.Round(result.GrossMonthlySalary, 2));
            Assert.AreEqual(Math.Round(result.NetAnnualSalary / 12, 2), Math.Round(result.NetMonthlySalary, 2));
            Assert.AreEqual(Math.Round(result.AnnualTax / 12, 2), Math.Round(result.MonthlyTax, 2));
        }
    }
}
