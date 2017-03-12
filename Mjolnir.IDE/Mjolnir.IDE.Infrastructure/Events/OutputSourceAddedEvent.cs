using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Events
{
    public class OutputSourceAddedEvent : PubSubEvent<OutputSourceAddedEvent>
    {
        public string OutputSourceName { get; set; }
    }
}
