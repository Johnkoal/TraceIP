using NLog;

namespace TraceIP.Infraestructure.Logger
{
    public class LoggerService : ILoggerService
    {
        #region "Propierties"
        private readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        #endregion

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, Exception e)
        {
            _logger.Error(message, e);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
