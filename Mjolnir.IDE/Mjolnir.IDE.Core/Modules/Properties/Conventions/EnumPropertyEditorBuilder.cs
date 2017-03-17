using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Properties.Inspectors;
using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties.Conventions
{
    public class EnumPropertyEditorBuilder : PropertyEditorBuilder
    {
        private readonly IUnityContainer _container;

        public EnumPropertyEditorBuilder(IUnityContainer container)
        {
            _container = container;
        }

        public override bool IsApplicable(PropertyDescriptor propertyDescriptor)
        {
            return typeof(Enum).IsAssignableFrom(propertyDescriptor.PropertyType);
        }

        public override IEditor BuildEditor(PropertyDescriptor propertyDescriptor)
        {
            return new EnumEditorViewModel(_container, propertyDescriptor.PropertyType);
        }
    }
}
