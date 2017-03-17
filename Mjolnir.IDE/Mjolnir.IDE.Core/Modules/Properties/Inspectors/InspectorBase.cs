using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.UI.Validation;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public abstract class InspectorBase : ValidatableBindableBase, IInspector
    {
        public abstract string Name { get; }
        public abstract bool IsReadOnly { get; }
    }
}