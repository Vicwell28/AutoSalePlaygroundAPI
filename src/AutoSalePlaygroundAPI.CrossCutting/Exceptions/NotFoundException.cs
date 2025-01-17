﻿namespace AutoSalePlaygroundAPI.CrossCutting.Exceptions
{
    /// <summary>
    /// Excepción para indicar que un recurso no se encontró.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
