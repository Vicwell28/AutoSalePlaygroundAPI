namespace AutoSalePlaygroundAPI.CrossCutting.Constants
{
    public static class ResponseCodes
    {
        // Éxito genérico
        public const string Success = "SUCCESS";

        // Validación
        public const string ValidationError = "VALIDATION_ERROR";

        // Lógica de negocio
        public const string NotFound = "NOT_FOUND";
        public const string Duplicate = "DUPLICATE";
        public const string Conflict = "CONFLICT";

        // Errores técnicos
        public const string UnexpectedError = "UNEXPECTED_ERROR";
        public const string DatabaseError = "DATABASE_ERROR";
        public const string ExternalServiceError = "EXTERNAL_SERVICE_ERROR";
    }
}