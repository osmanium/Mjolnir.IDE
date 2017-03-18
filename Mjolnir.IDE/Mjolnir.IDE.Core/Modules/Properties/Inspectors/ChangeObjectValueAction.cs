﻿using Mjolnir.IDE.Sdk;
using System.Globalization;
using System.Windows.Data;

namespace Mjolnir.IDE.Core.Modules.Properties.Inspectors
{
    public class ChangeObjectValueAction
    {
        private readonly BoundPropertyDescriptor _boundPropertyDescriptor;
        private readonly object _originalValue;
        private readonly object _newValue;
        private readonly IValueConverter _stringConverter;

        public string Name
        {
            get
            {
                string origText;
                string newText;

                if (_stringConverter != null)
                {
                    origText = (string) _stringConverter.Convert(_originalValue, typeof(string), null, CultureInfo.CurrentUICulture);
                    newText = (string) _stringConverter.Convert(_newValue, typeof(string), null, CultureInfo.CurrentUICulture);
                }
                else
                {
                    origText = _originalValue.ToString();
                    newText = _newValue.ToString();
                }

                return string.Format("Change {0} from {1} to {2}",
                    _boundPropertyDescriptor.PropertyDescriptor.DisplayName,
                    origText,
                    newText);
            }
        }

        public ChangeObjectValueAction(BoundPropertyDescriptor boundPropertyDescriptor, object newValue, IValueConverter stringConverter) :
            this(boundPropertyDescriptor, boundPropertyDescriptor.Value, newValue, stringConverter)
        { }

        public ChangeObjectValueAction(BoundPropertyDescriptor boundPropertyDescriptor, object originalValue, object newValue, IValueConverter stringConverter)
        {
            _boundPropertyDescriptor = boundPropertyDescriptor;
            _originalValue = originalValue;
            _newValue = newValue;
            _stringConverter = stringConverter;
        }

        public void Execute()
        {
            _boundPropertyDescriptor.Value = _newValue;
        }

        public void Undo()
        {
            _boundPropertyDescriptor.Value = _originalValue;
        }
    }
}