using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class AutoUnaviableException 
        : Exception
    {
        public AutoUnaviableException(string message) : base(message) { }

    }
}