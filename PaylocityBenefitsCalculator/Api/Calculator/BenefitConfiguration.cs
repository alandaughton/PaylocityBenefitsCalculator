namespace Api.Calculator
{
    /// <summary>
    /// The interface used to retrieve the configuration for benefit calculations.  The reason this is
    /// used as an interface is to enable unit testing.
    /// </summary>
    public interface IBenefitConfiguration
    {
        /// <summary>
        /// The base cost for an employee's benefits.
        /// </summary>
        decimal BaseBenefit { get; }

        /// <summary>
        /// The base cost for each dependent's benefits.
        /// </summary>
        decimal DependentBenefit { get; }

        /// <summary>
        /// The threshold salary above which an extra percentage cost is added for benefits.
        /// </summary>
        decimal SalaryThreshold { get; }

        /// <summary>
        /// The percentage cost added for benefits when a salary excedes the threshold.
        /// </summary>
        decimal AdditionalSalaryRate { get; }

        /// <summary>
        /// The threshold age above which an additional cost is added for dependent benefits.
        /// </summary>
        int DependentAgeThreshold { get; }

        /// <summary>
        /// The additional cost for dependent benefits when older than the age threshold.
        /// </summary>
        decimal AdditionalDependentAgeBenefit { get; }
    }

    /// <summary>
    /// The configuration for the benefit calculations will be in a databse or configuration
    /// file.  However, for the purposes of this code project the values will be hard-coded here.
    /// </summary>
    public class BenefitConfiguration : IBenefitConfiguration
    {
        private const decimal baseBenefit = 1000.00m;
        private const decimal dependentBenefit = 600.00m;
        private const decimal salaryThreshold = 80000.00m;
        private const decimal additionalSalaryRate = 0.02m;
        private const int dependentAgeThreshold = 50;
        private const decimal additionalDependentAgeBenefit = 200.00m;

        /// <summary>
        /// Force construction via the static factory method.
        /// </summary>
        private BenefitConfiguration() { }

        decimal IBenefitConfiguration.BaseBenefit => baseBenefit;

        decimal IBenefitConfiguration.DependentBenefit => dependentBenefit;

        decimal IBenefitConfiguration.SalaryThreshold => salaryThreshold;

        decimal IBenefitConfiguration.AdditionalSalaryRate => additionalSalaryRate;

        int IBenefitConfiguration.DependentAgeThreshold => dependentAgeThreshold;

        decimal IBenefitConfiguration.AdditionalDependentAgeBenefit => additionalDependentAgeBenefit;

        public static IBenefitConfiguration CreateConfiguration()
        {
            // If/when the configuration is moved to a database or configuration file, this
            // method can return a different class that pulls the configuration data from that source.
            return new BenefitConfiguration();
        }
    }
}
