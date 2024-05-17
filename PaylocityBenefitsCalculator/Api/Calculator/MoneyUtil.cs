namespace Api.Calculator
{
    /// <summary>
    /// Utility methods for handing monetary values.
    /// </summary>
    public static class MoneyUtil
    {
        public static decimal Round(decimal value)
        {
            // Away from zero is the typical rounding algorithm for financial calculations.
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
