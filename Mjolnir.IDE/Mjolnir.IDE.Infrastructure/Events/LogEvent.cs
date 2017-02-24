using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Events
{
    /// <summary>
    /// Class LogEvent - This event is used when a logging operation happens.
    /// </summary>
    public class LogEvent : PubSubEvent<IOutputService>
    {
    }
}
