using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunPEView.PEModel
{
    /// <summary>
    /// This class implements a PELogger as a singleton class.
    /// The behavior is event-based: to get notified about a new logging message, use the OnLog event.
    /// Logging messages are not stored, so after an event is fired for a new logging message, it's gone
    /// and there is no way to retrieve older messages.
    /// </summary>
    public sealed class PELogger
    {
        public enum LoggingLevel
        {
            INFO,
            WARNING,
            ERROR
        }

        /// <summary>
        /// the one and only instance
        /// </summary>
        private static readonly PELogger instance = new PELogger();

        /// <summary>
        /// private constructor for singleton pattern
        /// </summary>
        private PELogger() { }

        /// <summary>
        /// Only way to access the instance of the PELogger class is via this property.
        /// </summary>
        public static PELogger Instance
        {
            get
            {
                return instance;
            }
        }

        public delegate void LogEventDelegate(object sender, LoggingEventArgs e);

        public event LogEventDelegate logEvent;

        public void Log(string message, LoggingLevel logLevel)
        {
            if (logEvent != null)
            {
                logEvent(this, new LoggingEventArgs(message, logLevel));
            }
        }

        /// <summary>
        /// Event argument class for an logging event.
        /// </summary>
        public class LoggingEventArgs : EventArgs
        {
            private string message;
            private LoggingLevel logLevel;

            public LoggingEventArgs(string logMessage, LoggingLevel logLevel)
            {
                this.message = logMessage;
                this.logLevel = logLevel;
            }

            public LoggingLevel LogLevel
            {
                get { return logLevel; }
            }

            public string LogMessage
            {
                get { return message; }
            }
        }
    }
}
