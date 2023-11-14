using System.Diagnostics.CodeAnalysis;

namespace UnitTest.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class UnitTestException : Exception
    {
        public UnitTestException(string message) : base(message) { }
    }
}