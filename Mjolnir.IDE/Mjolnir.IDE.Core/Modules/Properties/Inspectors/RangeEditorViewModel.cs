using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class RangeEditorViewModel<T> : SelectiveUndoEditorBase<T>, ILabelledInspector, IEditor
    {
        private readonly T _minimum;
        private readonly T _maximum;

        public T Minimum
        {
            get { return _minimum; }
        }

        public T Maximum
        {
            get { return _maximum; }
        }

        public RangeEditorViewModel(IUnityContainer container, T minimum, T maximum)
            : base(container)
        {
            _minimum = minimum;
            _maximum = maximum;
        }

        public void DragStarted()
        {
            OnBeginEdit();
        }

        public void DragCompleted()
        {
            OnEndEdit();
        }
    }
}