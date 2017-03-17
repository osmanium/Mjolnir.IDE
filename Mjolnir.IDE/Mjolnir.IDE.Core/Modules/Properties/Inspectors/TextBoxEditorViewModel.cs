using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class TextBoxEditorViewModel<T> : EditorBase<T>, ILabelledInspector
    {
        private readonly IUnityContainer _container;

        public TextBoxEditorViewModel(IUnityContainer container)
            : base(container)
        {
            _container = container;
        }
    }
}