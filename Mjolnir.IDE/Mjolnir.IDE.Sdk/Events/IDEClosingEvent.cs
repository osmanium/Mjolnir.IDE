using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    public class IDEClosingEvent : PubSubEvent<IDEClosingEvent>
    {
        public bool IsPreventCloseHandled { get; set; }
    }
}
