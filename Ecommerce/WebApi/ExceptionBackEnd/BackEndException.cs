using System;

namespace WebApi.ExceptionBackEnd
{
    public class BackEndException : Exception
    {
        public BackEndException(string message) : base(message)
        {

        }
    }
}