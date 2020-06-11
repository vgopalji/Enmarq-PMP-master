using System;
using NLog;

namespace CareStream.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogError(Exception ex)
        {
            var errorDetails = $"{ex.ToString()}. Message: {ex.Message}. Type: {ex.GetType()}. Source: {ex.Source}. \n StackTrace: {ex.StackTrace}";
            LogError(errorDetails);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
