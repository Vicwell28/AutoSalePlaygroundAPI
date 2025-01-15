using Ardalis.GuardClauses;

namespace AutoSalePlaygroundAPI.CrossCutting.Extensions
{
    public static class CustomGuardExtensions
    {
        public static void MustBeTrue(this IGuardClause guardClause, bool condition, string parameterName)
        {
            if (!condition)
            {
                throw new ArgumentException($"The condition for {parameterName} must be true.");
            }
        }
    }
}
