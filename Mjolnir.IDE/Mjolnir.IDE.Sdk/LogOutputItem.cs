using Mjolnir.IDE.Sdk.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk
{
    public class LogOutputItem
    {
        /// <summary>
        /// Gets the message which just got logged.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; }

        /// <summary>
        /// Gets the category of logging.
        /// </summary>
        /// <value>The category.</value>
        public OutputCategory Category { get; }

        /// <summary>
        /// Gets the priority of logging.
        /// </summary>
        /// <value>The priority.</value>
        public OutputPriority Priority { get; }


        public string OutputSource { get; }

        public LogOutputItem(string message, OutputCategory category, OutputPriority priority, string outputSource = null)
        {
            this.Message = message;
            this.Category = category;
            this.Priority = priority;
            this.OutputSource = outputSource;
        }
    }
}
