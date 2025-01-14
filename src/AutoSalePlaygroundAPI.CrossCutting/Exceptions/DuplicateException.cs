using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.CrossCutting.Exceptions
{
    /// <summary>
    /// Excepción para indicar que se intenta insertar un recurso duplicado.
    /// </summary>
    public class DuplicateException : Exception
    {
        public DuplicateException(string message) : base(message)
        {
        }

        public DuplicateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}