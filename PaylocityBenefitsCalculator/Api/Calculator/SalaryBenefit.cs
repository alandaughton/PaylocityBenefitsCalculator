using Api.Models;

namespace Api.Calculator
{
    /// <summary>
    /// Implementation of the additional benefit cost for a salary beyond the threshold.
    /// </summary>
    /// <remarks>
    /// This requirement doesn't use the phrase 'per month' as do all the other requirements for benefit costs.
    /// Since 2% * 12 is very large, I'm going to assume this cost is per year and not per month.
    /// </remarks>
    public class SalaryBenefit : IBenefitCost
    {
        private IBenefitConfiguration _configuration;

        public SalaryBenefit(IBenefitConfiguration configuration)
        {
            this._configuration = configuration;
        }

        decimal IBenefitCost.CalculateCost(Employee employee, int year)
        {
            if (employee.Salary < this._configuration.SalaryThreshold)
            {
                return decimal.Zero;
            }

            return MoneyUtil.Round(this._configuration.AdditionalSalaryRate * employee.Salary);
        }
    }
}
