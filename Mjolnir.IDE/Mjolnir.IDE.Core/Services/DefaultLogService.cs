using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
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
        public void LogOutput(LogOutputItem log)
        {
            var trace = new StackTrace();
            StackFrame frame = trace.GetFrame(1); // 0 will be the inner-most method
            MethodBase method = frame.GetMethod();
            
            Logger.Log(LogLevel.Info, method.DeclaringType + ": " + log.Message);
            
            _aggregator.GetEvent<LogOutputEvent>().Publish(log);
        }

        public void AddOutputSource(string outputSource)
        {
            _aggregator.GetEvent<OutputSourceAddedEvent>().Publish(new OutputSourceAddedEvent() { OutputSourceName = outputSource });
        }

        public void RemoveOutputSource(string outputSource)
        {
            _aggregator.GetEvent<OutputSourceAddedEvent>().Publish(new OutputSourceAddedEvent() { OutputSourceName = outputSource });
        }
        #endregion
    }
}