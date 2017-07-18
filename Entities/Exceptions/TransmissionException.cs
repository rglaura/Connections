using Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Exceptions
{
    public class TransmissionException<T> : Exception where T : ITransmissionException
    {
        public TransmissionException() { }

        public TransmissionException(string message) : base(message) { }

        public TransmissionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
