using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSalePlaygroundAPI.CrossCutting.Exceptions
{
    public class DatabaseConcurrencyException : Exception
    {
        public DatabaseConcurrencyException(string message) : base(message) { }

        public DatabaseConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}