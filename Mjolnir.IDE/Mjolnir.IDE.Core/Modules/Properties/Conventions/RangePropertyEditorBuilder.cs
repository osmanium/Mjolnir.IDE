using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Properties.Inspectors;
using Mjolnir.IDE.Sdk.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties.Conventions
{
    public class RangePropertyEditorBuilder : PropertyEditorBuilder
    {
        private readonly IUnityContainer _container;

        public RangePropertyEditorBuilder(IUnityContainer container)
        {
            _container = container;
        }

        public override bool IsApplicable(PropertyDescriptor propertyDescriptor)
        {
            var isNumberType = propertyDescriptor.PropertyType == typeof(int)
                || propertyDescriptor.PropertyType == typeof(double)
                || propertyDescriptor.PropertyType == typeof(float);

            if (!isNumberType)
                return false;

            return propertyDescriptor.Attributes.Cast<Attribute>().Any(x => x is RangeAttribute);
        }

        public override IEditor BuildEditor(PropertyDescriptor propertyDescriptor)
        {
            var rangeAttribute = propertyDescriptor.Attributes
                .Cast<Attribute>().OfType<RangeAttribute>()
                .First();

            if (propertyDescriptor.PropertyType == typeof(int))
                return new RangeEditorViewModel<int>(_container,(int)rangeAttribute.Minimum, (int)rangeAttribute.Maximum);

            if (propertyDescriptor.PropertyType == typeof(double))
                return new RangeEditorViewModel<double>(_container, (double)rangeAttribute.Minimum, (double)rangeAttribute.Maximum);

            if (propertyDescriptor.PropertyType == typeof(float))
                return new RangeEditorViewModel<float>(_container, (float)rangeAttribute.Minimum, (float)rangeAttribute.Maximum);

            throw new InvalidOperationException();
        }
    }
}
