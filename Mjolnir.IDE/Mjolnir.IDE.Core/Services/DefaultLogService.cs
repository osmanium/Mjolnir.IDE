using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using NLog;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Services
{
    /// <summary>
    /// The NLogService for logging purposes
    /// </summary>
    public class DefaultLogService : IOutputService
    {
        private static readonly Logger Logger = LogManager.GetLogger("Mjolnir.IDE");
        private readonly IEventAggregator _aggregator;

        /// <summary>
        /// Private constructor of NLogService
        /// </summary>
        private DefaultLogService()
        {
        }

        /// <summary>
        /// The NLogService constructor
        /// </summary>
        /// <param name="aggregator">The injected event aggregator</param>
        public DefaultLogService(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        #region ILoggerService Members

        /// <summary>
        /// The logging function
        /// </summary>
        /// <param name="message">A message to log</param>
        /// <param name="category">The category of logging</param>
        /// <param name="priority">The priority of logging</param>
        public void LogOutput(string message, OutputCategory category, OutputPriority priority, string outputSource = null)
        {
            Message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), message);
            Category = category;
            Priority = priority;

            var trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1); // 0 will be the inner-most method
            MethodBase method = frame.GetMethod();

            Logger.Log(LogLevel.Info, method.DeclaringType + ": " + message);

            //TODOD : DefaultLogService
            _aggregator.GetEvent<LogEvent>().Publish(new DefaultLogService
            { Message = Message, Category = Category, Priority = Priority, OutputSource = outputSource });
        }

        public void AddOutputSource(string outputSource)
        {
            _aggregator.GetEvent<OutputSourceAddedEvent>().Publish(new OutputSourceAddedEvent() { OutputSourceName = outputSource });
        }

        public void RemoveOutputSource(string outputSource)
        {
            _aggregator.GetEvent<OutputSourceAddedEvent>().Publish(new OutputSourceAddedEvent() { OutputSourceName = outputSource });
        }


        /// <summary>
        /// The message which was last logged using the service
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// The log message's category
        /// </summary>
        public OutputCategory Category { get; internal set; }

        /// <summary>
        /// The log message's priority
        /// </summary>
        public OutputPriority Priority { get; internal set; }

        public string OutputSource { get; set; }

        #endregion
    }
}