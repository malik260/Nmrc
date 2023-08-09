﻿namespace Mortgage.Ecosystem.DataAccess.Layer.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        {
        }

        public LogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
