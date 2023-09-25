using System;

namespace WebApiTest.Exceptions
{
    public class TestException : Exception
    {
        public TestException(string message) : base(message)
        {

        }


    }
}