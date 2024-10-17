namespace TraceIP.Infraestructure.Exceptions
{
    public class ExceptionInfoRequire : Exception
    {
        public ExceptionInfoRequire(string message) : base(message) { }

        public ExceptionInfoRequire(string message, Exception ex) : base(message, ex) { }
    }
}
