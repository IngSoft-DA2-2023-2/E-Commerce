namespace LogicInterface.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(Exception innerException) : base(innerException.Message, innerException) { }
        public LogicException(string message) : base(message) { }
    }
}