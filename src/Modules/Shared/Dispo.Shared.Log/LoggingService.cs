using Serilog;

namespace Dispo.Shared.Log
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService(ILogger logger)
        {
            _logger = logger;
        }

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Information(string message, params object?[]? properties)
        {
            _logger.Information(message, properties);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Warning(string message, params object?[]? properties)
        {
            _logger.Warning(message, properties);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(string message, params object?[]? properties)
        {
            _logger.Error(message, properties);
        }
    }
}
