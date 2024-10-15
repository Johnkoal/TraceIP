namespace TraceIP.Infraestructure.Logger
{
    public interface ILoggerService
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(string message, Exception ex);
        void Fatal(string message);
    }
}
