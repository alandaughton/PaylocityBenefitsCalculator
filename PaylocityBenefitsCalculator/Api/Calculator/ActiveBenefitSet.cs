namespace Api.Calculator
{
    using System.Collections.Generic;

    public interface IActiveBenefitSet
    {
        IEnumerable<IBenefitCost> ActiveBenefitCosts { get; }
    }

    /// <summary>
    /// Implementation of the set of active benefits that should be included in the benefits cost calculation.
    /// </summary>
    public class ActiveBenefitSet : IActiveBenefitSet
    {
        private IBenefitConfiguration _configuration;

        private ActiveBenefitSet(IBenefitConfiguration configuration)
        {
            this._configuration = configuration;
        }

        IEnumerable<IBenefitCost> IActiveBenefitSet.ActiveBenefitCosts
        {
            get
            {
                return new List<IBenefitCost>()
                {
                    new BaseEmployeeBenefit(this._configuration),
                    new BaseDependentBenefit(this._configuration),
                    new SalaryBenefit(this._configuration),
                    new AgeBenefit(this._configuration)
                };
            }
        }

        /// <summary>
        /// Factory method for retrieving the list of active benefits.
        /// </summary>
        /// <remarks>
        /// The factory method is here because it may be desirable to alter the source of which benefits to
        /// include, for example to a database or configuration file.  In such case, this can return a different
        /// implementation of the interface.
        /// </remarks>
        public static IActiveBenefitSet RetrieveActiveBenefits(IBenefitConfiguration configuration)
        {
            return new ActiveBenefitSet(configuration);
        }
    }
}
