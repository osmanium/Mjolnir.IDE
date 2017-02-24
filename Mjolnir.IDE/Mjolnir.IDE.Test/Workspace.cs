using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mjolnir.IDE.Test
{
    public class Workspace : AbstractWorkspace
    {
        /// <summary>
        /// The generic workspace that will be used if the application does not have its workspace
        /// </summary>
        /// <param name="container">The injected container - can be used by custom flavors of workspace</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public Workspace(IUnityContainer container, IEventAggregator eventAggregator, IApplicationDefinition applicationDefinition)
            : base(container, eventAggregator, applicationDefinition)
        {
        }
    }
}