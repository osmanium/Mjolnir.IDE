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
    public abstract class PropertyEditorBuilder
    {
        //private readonly IUnityContainer _container;

        public PropertyEditorBuilder()
        {
            //_container = container;
        }

        public abstract bool IsApplicable(PropertyDescriptor propertyDescriptor);
        public abstract IEditor BuildEditor(PropertyDescriptor propertyDescriptor);
    }
}
