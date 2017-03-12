﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Events
{
    public class OutputSourceRemovedEvent : PubSubEvent<OutputSourceRemovedEvent>
    {
        public string OutputSourceName { get; set; }
    }
}
