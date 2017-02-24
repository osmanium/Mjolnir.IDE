using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Prism.Events;

namespace Mjolnir.IDE.Infrastructure
{
    public class DefaultWorkspace : AbstractWorkspace
    {
        public DefaultWorkspace(IUnityContainer container, IEventAggregator eventAggregator, IApplicationDefinition applicationDefinition)
            : base(container, eventAggregator, applicationDefinition)
        {
        }
    }
}
