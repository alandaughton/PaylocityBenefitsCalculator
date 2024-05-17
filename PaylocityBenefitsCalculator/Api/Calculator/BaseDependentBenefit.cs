namespace Api.Calculator
{
    using Api.Models;

    /// <summary>
    /// Implementation of the base dependent benefit cost.
    /// </summary>
    public class BaseDependentBenefit : IBenefitCost
    {
        private IBenefitConfiguration _configuration;

        public BaseDependentBenefit(IBenefitConfiguration configuration)
        {
            this._configuration = configuration;
        }

        decimal IBenefitCost.CalculateCost(Employee employee, int year)
        {
            return 12 * employee.Dependents.Count() * this._configuration.DependentBenefit;
        }
    }
}
