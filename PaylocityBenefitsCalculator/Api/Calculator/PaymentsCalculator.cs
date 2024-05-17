namespace Api.Calculator
{
    using Api.Models;

    /// <summary>
    /// Calculate the payments for an employee for a year.
    /// </summary>
    public class PaymentsCalculator
    {
        private const int NumberOfPayments = 26;

        private IActiveBenefitSet _benefitCosts;

        public PaymentsCalculator(IActiveBenefitSet benefitSet)
        {
            this._benefitCosts = benefitSet;
        }

        public IEnumerable<decimal> CalculatePayments(Employee employee, int year)
        {
            var benefitCalculator = new YearlyBenefitCalculator(this._benefitCosts);
            var yearlyCost = benefitCalculator.CalculateYearlyCosts(employee, year);

            var salaryLessBenefits = employee.Salary - yearlyCost;
            var perPaymentAmount = MoneyUtil.Round(salaryLessBenefits / NumberOfPayments);

            // There may now be a rounding error that, in cents, will be less than the NumberOfPayments value.
            // We wish to spread this adjustment evenly through the year.
            var diff = salaryLessBenefits - NumberOfPayments * perPaymentAmount;
            bool isNegative = diff < decimal.Zero;

            var diffInCents = (isNegative ? -1 : 1) * Convert.ToInt32(diff * 100);
            var gap = (diffInCents == 0) ? NumberOfPayments : NumberOfPayments / diffInCents;

            var listPayments = new List<decimal>();
            for (int i = 0; i < NumberOfPayments; ++i)
            {
                var payment = perPaymentAmount;
                if ((i + 1) % gap == 0)
                {
                    payment += isNegative ? -0.01m : 0.01m;
                }

                listPayments.Add(payment);
            }

            return listPayments;
        }
    }
}
