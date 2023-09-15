using System;

namespace BackEnd.ExceptionBackEnd
{
    public class BackEndException : Exception
    {
        public BackEndException(string message) : base(message)
        {

        }
    }
}