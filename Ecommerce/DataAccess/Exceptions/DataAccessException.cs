namespace DataAccess.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message) : base(message)
        {

        }
        public DataAccessException() : base()
        {

        }
    }
}
