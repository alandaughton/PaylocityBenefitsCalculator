namespace Api.Calculator
{
    using Api.Models;

    /// <summary>
    /// Implementation of the base employee benefit cost.
    /// </summary>
    public class BaseEmployeeBenefit : IBenefitCost
    {
        private IBenefitConfiguration _configuration;

        public BaseEmployeeBenefit(IBenefitConfiguration configuration)
        {
            this._configuration = configuration;
        }

        decimal IBenefitCost.CalculateCost(Employee employee, int year)
        {
            return 12 * this._configuration.BaseBenefit;
        }
    }
}
