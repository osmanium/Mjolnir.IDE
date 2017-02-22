using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure
{
    public class Workspace : AbstractWorkspace
    {
        /// <summary>
        /// The generic workspace that will be used if the application does not have its workspace
        /// </summary>
        /// <param name="container">The injected container - can be used by custom flavors of workspace</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Workspace(IUnityContainer container, IEventAggregator eventAggregator)
            : base(container, eventAggregator)
        {
        }
    }
}