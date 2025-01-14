namespace AutoSalePlaygroundAPI.CrossCutting.Constants
{
    public static class ValidationConstants
    {
        // Ejemplo de longitud
        public const int MaxMarcaLength = 100;
        public const int MaxModeloLength = 100;

        // Ejemplo de rangos
        public const int MinYear = 1900;
        public const int MaxYear = 2100;

        // Ejemplo de patrones
        public const string EmailRegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    }
}
