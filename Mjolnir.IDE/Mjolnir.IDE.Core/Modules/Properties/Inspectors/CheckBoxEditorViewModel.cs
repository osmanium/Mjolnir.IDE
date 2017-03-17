using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class CheckBoxEditorViewModel : EditorBase<bool>, ILabelledInspector
    {
        private IUnityContainer _container;
        public CheckBoxEditorViewModel(IUnityContainer container)
            : base(container)
        {
            _container = container;
        }
    }
}