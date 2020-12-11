using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class KlantManagerException : Exception
    {
        public KlantManagerException(string message) : base(message)
        {
        }

        public KlantManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
