using Mjolnir.IDE.Infrastructure.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Events
{
    /// <summary>
    /// Class OpenContentEvent - This event happens when a new document is opened.
    /// </summary>
    public class ClosedContentEvent : PubSubEvent<ContentViewModel>
    {
    }
}
