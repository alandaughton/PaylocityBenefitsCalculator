namespace Api.Calculator
{
    using Api.Models;

    /// <summary>
    /// Each benefit will be implemented with a class that inherits from this interface,
    /// which will be called in sequence for every configured benefit.
    /// </summary>
    /// <remarks>
    /// This value is the per year benefit cost.
    /// </remarks>
    public interface IBenefitCost
    {
        decimal CalculateCost(Employee employee, int year);
    }
}
