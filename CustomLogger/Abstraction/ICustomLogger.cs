namespace CustomLogger.Abstraction
{
    public interface ICustomLogger<T> where T : class
    {
        void LogTrace(string message);
        void LogTrace(string message, params object[] args);
        void LogInformation(string message);
        void LogInformation(string message, params object[] args);
        void LogWarning(string message);
        void LogWarning(string message, params object[] args);
        void LogDebug(string message);
        void LogDebug(string message, params object[] args);
        void LogError(string message);
        void LogError(string message, params object[] args);
        void LogError(Exception ex, string message);
        void LogError(Exception ex, string message, params object[] args);
        void LogCritical(string message);
        void LogCritical(string message, params object[] args);
        void LogCritical(Exception ex, string message);
        void LogCritical(Exception ex, string message, params object[] args);

    }
}
