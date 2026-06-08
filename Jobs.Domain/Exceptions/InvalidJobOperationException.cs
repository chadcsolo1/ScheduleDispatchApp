using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Domain.Exceptions
{
    public class InvalidJobOperationException : Exception
    {
        public InvalidJobOperationException(string message)
            : base(message)
        {
        }

        public InvalidJobOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
