namespace AutoSalePlaygroundAPI.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static int? ToNullableInt(this string? value)
        {
            if (int.TryParse(value, out var result))
                return result;

            return null;
        }

        public static string? Truncate(this string? value, int maxLength)
        {
            if (value == null) return null;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
