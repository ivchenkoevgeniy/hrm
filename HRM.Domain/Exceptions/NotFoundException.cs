using System;

namespace HRM.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}