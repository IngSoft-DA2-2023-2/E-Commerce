using System.Diagnostics.CodeAnalysis;

namespace WebApiTest.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class TestException : Exception
    {
        public TestException(string message) : base(message)
        {

        }


    }
}