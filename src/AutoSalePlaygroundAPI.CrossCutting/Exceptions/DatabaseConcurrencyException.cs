namespace AutoSalePlaygroundAPI.CrossCutting.Exceptions
{
    public class DatabaseConcurrencyException : Exception
    {
        public DatabaseConcurrencyException(string message) : base(message) { }

        public DatabaseConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}