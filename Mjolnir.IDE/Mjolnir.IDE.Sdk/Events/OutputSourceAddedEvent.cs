﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    public class OutputSourceAddedEvent : PubSubEvent<OutputSourceAddedEvent>
    {
        public string OutputSourceName { get; set; }

        public OutputSourceAddedEvent()
        { }

        public OutputSourceAddedEvent(string outputSourceName)
        {
            this.OutputSourceName = outputSourceName;
        }
    }
}
