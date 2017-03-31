using Mjolnir.IDE.Sdk.Interfaces.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    public class LogOutputEvent : PubSubEvent<LogOutputItem>
    {
        public LogOutputItem Log { get; set; }
    }
}
