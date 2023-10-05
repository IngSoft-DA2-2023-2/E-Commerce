using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class TestException : Exception
    {
        public TestException(string message) : base(message)
        {

        }


    }
}