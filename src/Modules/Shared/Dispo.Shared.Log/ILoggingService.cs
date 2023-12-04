namespace Dispo.Shared.Log
{
    public interface ILoggingService
    {
        void Information(string message);
        void Information(string message, params object?[]? properties);
        void Warning(string message);
        void Warning(string message, params object?[]? properties);
        void Error(string message);
        void Error(string message, params object?[]? properties);
    }
}
