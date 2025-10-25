namespace CustomLogger.Abstraction
{
    public interface ICustomLogger<T> where T : class
    {
        void LogInformation(string message);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message);
        void LogWarning(string message, params object[] args);
        void LogError(string message, Exception ex);
        void LogError(string message, params object[] args);
        void LogDebug(string message);
        void LogDebug(string message, params object[] args);
        void LogCritical(string message);
        void LogCritical(string message, params object[] args);
        void LogTrace(string message);
        void LogTrace(string message, params object[] args);
    }
}
