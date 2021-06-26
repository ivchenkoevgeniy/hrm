using System;

namespace HRM.Domain.Exceptions
{
    public class NotModifiedException : Exception
    {
        public NotModifiedException(string msg) : base(msg)
        {
        }
    }
}