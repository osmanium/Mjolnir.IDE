using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Services
{
    /// <summary>
    /// Interface ILoggerService - used for logging in the application
    /// </summary>
    public interface IOutputService
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The logging category.</param>
        /// <param name="priority">The logging priority.</param>
        void LogOutput(LogOutputItem log);

        void AddOutputSource(string outputSource);
        void RemoveOutputSource(string outputSource);
    }
}
