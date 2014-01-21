using System;
using System.Collections.Concurrent;
using System.Linq;

namespace OfficeAvenger.Services.Logging
{
    /// <summary>
    /// Custom interface for logging messages
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Detailed diagnostic messages for development. Not normally written to the log file, but only to console.
        /// The lazy overload Debug(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Debug(string message, params object[] formatting);

        /// <summary>
        /// Detailed diagnostic messages for development. Not normally written to the log file, but only to console.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(Func<string> message);

        /// <summary>
        /// Exceptions that prevent normal use of the application.
        /// The lazy overload Error(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Error(string message, params object[] formatting);

        /// <summary>
        /// Exceptions that prevent normal use of the application.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(Func<string> message);

        /// <summary>
        /// Severe errors that typically cause termination of the application.
        /// The lazy overload Fatal(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Fatal(string message, params object[] formatting);

        /// <summary>
        /// Severe errors that typically cause termination of the application.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(Func<string> message);

        /// <summary>
        /// Runtime events such as initialization, startup and shutdown.
        /// The lazy overload Info(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Info(string message, params object[] formatting);

        /// <summary>
        /// Runtime events such as initialization, startup and shutdown.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(Func<string> message);

        /// <summary>
        /// Initializes the instance for the logger name
        /// </summary>
        /// <param name="loggerName">Name of the logger</param>
        void InitializeFor(string loggerName);

        /// <summary>
        /// Unexpected results or exceptions where the application is able to continue executing on its on.
        /// The lazy overload Warn(() => message) is preferred since the execution is deferred.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatting">The formatting.</param>
        void Warn(string message, params object[] formatting);

        /// <summary>
        /// Unexpected results or exceptions where the application is able to continue executing on its on.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warn(Func<string> message);
    }

    /// <summary>
    /// Ensures a default constructor for the logger type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILog<T> where T : new()
    {
    }

    /// <summary>
    /// Logger type initialization
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Initializes a new instance of a logger for an object.
        /// This should be done only once per object name.
        /// </summary>
        /// <param name="objectName">Name of the object.</param>
        /// <returns>ILog instance for an object if log type has been intialized; otherwise null</returns>
        public static ILog GetLoggerFor(string objectName)
        {
            var logger = _logger;

            if (_logger == null)
            {
                logger = Activator.CreateInstance(_logType) as ILog;
                if (logger != null)
                {
                    logger.InitializeFor(objectName);
                }
            }

            return logger;
        }

        /// <summary>
        /// Sets up logging to be with a certain type
        /// </summary>
        /// <typeparam name="T">The type of ILog for the application to use</typeparam>
        public static void InitializeWith<T>() where T : ILog, new()
        {
            _logType = typeof(T);
        }

        /// <summary>
        /// Sets up logging to be with a certain instance. The other method is preferred.
        /// </summary>
        /// <param name="loggerType">Type of the logger.</param>
        /// <remarks>This is mostly geared towards testing</remarks>
        public static void InitializeWith(ILog loggerType)
        {
            _logType = loggerType.GetType();
            _logger = loggerType;
        }

        private static ILog _logger;
        private static Type _logType = typeof(NullLog);
    }

    /// <summary>
    /// Extensions to help make logging awesome
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Formats string with the formatting passed in. This is a shortcut to string.Format().
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="formatting">The formatting.</param>
        /// <returns>A formatted string.</returns>
        public static string FormatWith(this string input, params object[] formatting)
        {
            return string.Format(input, formatting);
        }

        /// <summary>
        /// Gets the logger for <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type to get the logger for.</param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log<T>(this T type)
        {
            string objectName = typeof(T).FullName;
            return Log(objectName);
        }

        /// <summary>
        /// Gets the logger for the specified object name.
        /// </summary>
        /// <param name="objectName">Either use the fully qualified object name or the short. If used with Log&lt;T&gt;() you must use the fully qualified object name"/></param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log(this string objectName)
        {
            return _dictionary.Value.GetOrAdd(objectName, Logging.Log.GetLoggerFor);
        }

        /// <summary>
        /// Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly Lazy<ConcurrentDictionary<string, ILog>> _dictionary = new Lazy<ConcurrentDictionary<string, ILog>>(() => new ConcurrentDictionary<string, ILog>());
    }
}