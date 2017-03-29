using Mjolnir.IDE.Sdk.Interfaces.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    //TODO : It should send log object not the service
    public class LogEvent : PubSubEvent<IOutputService>
    {
    }
}
