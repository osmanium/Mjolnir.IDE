using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Services
{
    /// <summary>
    /// Enum LogCategory
    /// </summary>
    public enum OutputCategory
    {
        Debug,
        Exception,
        Info,
        Warn,
        Error
    }

    /// <summary>
    /// Enum LogPriority
    /// </summary>
    public enum OutputPriority
    {
        None,
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Interface ILoggerService - used for logging in the application
    /// </summary>
    public interface IOutputService
    {
        /// <summary>
        /// Gets the message which just got logged.
        /// </summary>
        /// <value>The message.</value>
        string Message { get; }

        /// <summary>
        /// Gets the category of logging.
        /// </summary>
        /// <value>The category.</value>
        OutputCategory Category { get; }

        /// <summary>
        /// Gets the priority of logging.
        /// </summary>
        /// <value>The priority.</value>
        OutputPriority Priority { get; }


        string OutputSource { get; }


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The logging category.</param>
        /// <param name="priority">The logging priority.</param>
        void LogOutput(string message, OutputCategory category, OutputPriority priority, string outputSource = null);

        void AddOutputSource(string outputSource);
        void RemoveOutputSource(string outputSource);
    }
}
