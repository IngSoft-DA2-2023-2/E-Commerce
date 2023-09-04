using System;

namespace BackEnd
{
    public class BackEndException : Exception
    {
        public BackEndException(string message) : base(message)
        {

        }
    }
}