namespace Api.Calculator
{
    using System.Linq;
    using Api.Models;

    /// <summary>
    /// Calculate the cost of benefits for a year.
    /// </summary>
    public class YearlyBenefitCalculator
    {
        private IActiveBenefitSet _benefitCosts;

        public YearlyBenefitCalculator(IActiveBenefitSet benefitSet)
        {
            _benefitCosts = benefitSet;
        }

        public decimal CalculateYearlyCosts(Employee employee, int year)
        {
            var totalCost = this._benefitCosts.ActiveBenefitCosts.Select(b => b.CalculateCost(employee, year)).Sum();
            return MoneyUtil.Round(totalCost);
        }
    }
}
