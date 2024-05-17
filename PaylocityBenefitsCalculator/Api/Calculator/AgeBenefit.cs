using Api.Models;
using System.Security.Principal;

namespace Api.Calculator
{
    /// <summary>
    /// Implementation of the additional benefit cost for dependents older than the threshold.
    /// </summary>
    public class AgeBenefit : IBenefitCost
    {
        private IBenefitConfiguration _configuration;

        public AgeBenefit(IBenefitConfiguration configuration)
        {
            this._configuration = configuration;
        }

        decimal IBenefitCost.CalculateCost(Employee employee, int year)
        {
            // The requirements do not specify what to do for the month in which the threshold
            // birthday occurs.  I will assume that the full month's value is due for that month.

            decimal total = 0.00m;
            foreach (var dependent in employee.Dependents)
            {
                int thresholdYear = dependent.DateOfBirth.Year + this._configuration.DependentAgeThreshold;
                if (thresholdYear < year)
                {
                    total += 12 * this._configuration.AdditionalDependentAgeBenefit;
                }
                else if (thresholdYear == year)
                {
                    total += (12 - dependent.DateOfBirth.Month + 1) * this._configuration.AdditionalDependentAgeBenefit;
                }
            }

            return total;
        }
    }
}
