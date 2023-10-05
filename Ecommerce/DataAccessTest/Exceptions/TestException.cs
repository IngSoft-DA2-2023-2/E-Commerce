using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class TestException : Exception
    {
        public TestException(string message) : base(message)
        {

        }


    }
}