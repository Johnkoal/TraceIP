namespace TraceIP.Infraestructure.Exceptions
{
    public class ExceptionDatabase : Exception
    {
        public ExceptionDatabase(string message) : base(message) { }

        public ExceptionDatabase(string message, Exception ex) : base(message, ex) { }
    }
}
