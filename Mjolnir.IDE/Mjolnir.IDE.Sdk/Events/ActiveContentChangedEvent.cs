using Mjolnir.IDE.Sdk.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    /// <summary>
    /// Class ActiveContentChangedEvent - This event is used when the active content is changed in Avalon Dock.
    /// </summary>
    public class ActiveContentChangedEvent : PubSubEvent<ContentViewModel>
    {
    }
}
