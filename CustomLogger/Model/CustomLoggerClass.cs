using CustomLogger.Abstraction;
using CustomLogger.Enum;
using CustomLogger.Helper;
using System.Text;

namespace CustomLogger.Model
{
    public sealed class CustomLoggerClass<T> : ICustomLogger<T> where T : class
    {
        #region fields
        private static object _Consolelock = new object();
        #endregion
        public CustomLoggerClass()
        {
        }
        #region Trace Logs
        public void LogTrace(string message)
        {
            GenerateMessage(ELogLevel.Debug, message);
        }

        public void LogTrace(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Debug, message, args);
        }
        #endregion

        #region Information Logs
        public void LogInformation(string message)
        {
            GenerateMessage(ELogLevel.Info, message);
        }

        public void LogInformation(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Info, message, args);
        }
        #endregion

        #region Warning Logs
        public void LogWarning(string message)
        {
            GenerateMessage(ELogLevel.Warning, message);
        }

        public void LogWarning(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Warning, message, args);
        }
        #endregion

        #region Debug Logs
        public void LogDebug(string message)
        {
            GenerateMessage(ELogLevel.Debug, message);
        }

        public void LogDebug(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Debug, message, args);
        }
        #endregion

        #region Error Logs
        public void LogError(string message)
        {
            GenerateMessage(ELogLevel.Error, message);
        }
        public void LogError(Exception ex, string message)
        {
            GenerateMessage(ELogLevel.Error, ex, message);
        }
        public void LogError(Exception ex, string message,  params object[] args)
        {
            GenerateMessage(ELogLevel.Error, ex, message, args);
        }
        public void LogError(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Error, message, args);
        }
        #endregion

        #region Critical Logs
        public void LogCritical(string message)
        {
            GenerateMessage(ELogLevel.Critical, message);
        }

        public void LogCritical(string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Critical, message, args);
        }
        public void LogCritical(Exception ex, string message)
        {
            GenerateMessage(ELogLevel.Critical, message, ex);
        }   
        public void LogCritical(Exception ex, string message, params object[] args)
        {
            GenerateMessage(ELogLevel.Critical, message, ex, args);
        }
        #endregion

        #region methods
        private void GenerateMessage(ELogLevel logLevel, string message)
        {
            string formattedMessage = string.Empty;
            switch (logLevel)
            {
                case ELogLevel.Trace:
                    formattedMessage = $"{ColorHelper.Grey("[TRACE]")} {DateTime.UtcNow:o} - {message}";
                    break;

                case ELogLevel.Info:
                    formattedMessage = $"{ColorHelper.Grey("[INFO]")} {DateTime.UtcNow:o} - {message}";
                    break;
                case ELogLevel.Warning:
                    formattedMessage = $"{ColorHelper.Yellow("[WARNING]")} {DateTime.UtcNow:o} - {message}";
                    break;
                case ELogLevel.Error:
                    formattedMessage = $"{ColorHelper.Orange("[ERROR]")} {DateTime.UtcNow:o} - {message}";
                    break;
                case ELogLevel.Debug:
                    formattedMessage = $"{ColorHelper.Green("[DEBUG]")} {DateTime.UtcNow:o} - {message}";
                    break;
                case ELogLevel.Critical:
                    formattedMessage = $"{ColorHelper.Red("[CRITICAL]")} {DateTime.UtcNow:o} - {message}";
                    break;
                default:
                    formattedMessage = $"{ColorHelper.Red("[UNKNOWN]")} {DateTime.UtcNow:o} - {message}";
                    break;
            }
            WriteToConsole(formattedMessage);
        }

        private void GenerateMessage(ELogLevel logLevel, string message, params object[] args)
        {
            string formattedMessage = string.Empty;
            try
            {
                formattedMessage = string.Format(message, args);
            }
            catch (Exception)
            {
                formattedMessage = message;
            }
            GenerateMessage(logLevel, formattedMessage);
        }
        private void GenerateMessage(ELogLevel logLever, Exception ex, string message)
        {
            string formattedMessage = string.Empty;
            try
            {
                string strException = GenerateExceptionLog(ex);
                formattedMessage = $"{message} - Exception: {strException}";
            }
            catch (Exception)
            {
                formattedMessage = message;
            }
            GenerateMessage(logLever, formattedMessage);
        }

        private void GenerateMessage(ELogLevel logLever, Exception ex, string message, params object[] args)
        {
            string formattedMessage = string.Empty;
            try
            {
                formattedMessage = string.Format(message, args);
                string strException = GenerateExceptionLog(ex);
                string combined = $"{formattedMessage} - Exception: {strException}";
            }
            catch (Exception)
            {
                formattedMessage = message;
            }
            GenerateMessage(logLever, formattedMessage);
        } 
        private string GenerateExceptionLog(Exception ex)
        {
            if (ex == null) return string.Empty;

            var sb = new StringBuilder();
            int level = 0;
            for (var e = ex; e != null; e = e.InnerException, level++)
            {
                sb.AppendLine($"-- Exception Level {level} --");
                sb.AppendLine($"Type     : {e.GetType().FullName}");
                sb.AppendLine($"Message  : {e.Message}");
                sb.AppendLine($"HResult  : 0x{e.HResult:X8}");
                if (!string.IsNullOrWhiteSpace(e.Source))
                    sb.AppendLine($"Source   : {e.Source}");
                if (e.TargetSite != null)
                    sb.AppendLine($"Target   : {e.TargetSite}");
                if (e.StackTrace != null)
                {
                    sb.AppendLine("StackTrace:");
                    sb.AppendLine(e.StackTrace);
                }

                if (e.Data != null && e.Data.Count > 0)
                {
                    sb.AppendLine("Data:");
                    foreach (System.Collections.DictionaryEntry kv in e.Data)
                    {
                        sb.AppendLine($"  {kv.Key}: {kv.Value}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }
        private void WriteToConsole(string formattedMessage)
        {
            lock (_Consolelock)
            {
                Console.WriteLine(formattedMessage);
            }
        }
        #endregion
    }
}

