using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces;
using Prism.Events;

namespace Mjolnir.IDE.Sdk
{
    public class DefaultWorkspace : AbstractWorkspace
    {
        public DefaultWorkspace(IUnityContainer container, IEventAggregator eventAggregator, IApplicationDefinition applicationDefinition)
            : base(container, eventAggregator, applicationDefinition)
        {
        }
    }
}
