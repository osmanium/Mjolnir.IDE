﻿using Microsoft.Practices.Unity;
using System;


namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    /// <summary>
    /// This class is used for values that should only be updated after the
    /// user has finished editing them. The view needs to call OnBeginEdit when
    /// the user has started editing to capture the current value and call
    /// OnEndEdit to commit the old and new value to the undo / redo manager.
    /// </summary>
    /// <typeparam name="TValue">Type of the value</typeparam>
    public abstract class SelectiveUndoEditorBase<TValue> : EditorBase<TValue>, IDisposable
    {
        private object _originalValue = null;


        public SelectiveUndoEditorBase(IUnityContainer container)
            :base(container)
        {
        }

        protected void OnBeginEdit()
        {
            IsUndoEnabled = false;
            _originalValue = RawValue;
        }

        protected void OnEndEdit()
        {
            if (_originalValue == null)
                return;

            try
            {
                var value = RawValue;
                //TODO:Figure something out for here
                //if (!_originalValue.Equals(value))
                //    IoC.Get<IShell>().ActiveItem.UndoRedoManager.ExecuteAction(
                        //new ChangeObjectValueAction(BoundPropertyDescriptor, _originalValue, value, StringConverter));
            }
            finally
            {
                _originalValue = null;
                IsUndoEnabled = true;
            }
        }

        public override void Dispose()
        {
            OnEndEdit();
            base.Dispose();
        }
    }
}
