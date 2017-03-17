using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties.Conventions
{
    public class StandardPropertyEditorBuilder<T, TEditor> : PropertyEditorBuilder
        where TEditor : IEditor
    {
        private readonly IUnityContainer _container;

        public StandardPropertyEditorBuilder(IUnityContainer container)
        {
            _container = container;
        }

        public override bool IsApplicable(PropertyDescriptor propertyDescriptor)
        {
            return propertyDescriptor.PropertyType == typeof(T);
        }

        public override IEditor BuildEditor(PropertyDescriptor propertyDescriptor)
        {
            return _container.Resolve<TEditor>();
        }
    }
}
