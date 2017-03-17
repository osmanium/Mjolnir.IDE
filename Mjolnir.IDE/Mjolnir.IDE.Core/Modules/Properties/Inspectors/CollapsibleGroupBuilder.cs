using Microsoft.Practices.Unity;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class CollapsibleGroupBuilder : InspectorBuilder<CollapsibleGroupBuilder>
    {
        private IUnityContainer _container;

        public CollapsibleGroupBuilder(IUnityContainer container)
            :base(container)
        {
            _container = container;
        }

        internal CollapsibleGroupViewModel ToCollapsibleGroup(string name)
        {
            return new CollapsibleGroupViewModel(name, Inspectors);
        }
    }
}