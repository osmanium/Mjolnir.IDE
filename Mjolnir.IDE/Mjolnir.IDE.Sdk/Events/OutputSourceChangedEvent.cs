using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    public class OutputSourceChangedEvent : PubSubEvent<OutputSourceChangedEvent>
    {
        public string EventSourceName { get; set; }

        public OutputSourceChangedEvent()
        {

        }

        public OutputSourceChangedEvent(string eventSourceName)
        {
            this.EventSourceName = eventSourceName;
        }
    }
}
