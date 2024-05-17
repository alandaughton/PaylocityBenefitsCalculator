namespace Api.DataAccessLayer
{
    /// <summary>
    /// Exception used when a requested data item is not found.
    /// </summary>
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() { }

        public DataNotFoundException(string message) : base(message) { }

        public DataNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
