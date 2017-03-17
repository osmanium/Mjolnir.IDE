using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties
{
    public class InspectableObjectBuilder : InspectorBuilder<InspectableObjectBuilder>
    {
        private readonly IUnityContainer _container;

        public InspectableObjectBuilder(IUnityContainer container)
            :base(container)
        {
            _container = container;
        }

        public InspectableObject ToInspectableObject()
        {
            return new InspectableObject(Inspectors);
        }
    }
}
