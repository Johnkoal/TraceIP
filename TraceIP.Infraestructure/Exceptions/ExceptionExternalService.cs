namespace TraceIP.Infraestructure.Exceptions
{
    public class ExceptionExternalService : Exception
    {
        public ExceptionExternalService(string message) : base(message) { }

        public ExceptionExternalService(string message, Exception ex) : base(message, ex) { }
    }
}
