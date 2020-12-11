using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Exceptions
{
    public class BestellingManagerException : Exception
    {
        public BestellingManagerException(string message) : base(message)
        {
        }

        public BestellingManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
