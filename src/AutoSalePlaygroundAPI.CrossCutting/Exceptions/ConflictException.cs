﻿namespace AutoSalePlaygroundAPI.CrossCutting.Exceptions
{
    /// <summary>
    /// Excepción para indicar que hay un conflicto en la operación (p.ej. estado no válido).
    /// </summary>
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}