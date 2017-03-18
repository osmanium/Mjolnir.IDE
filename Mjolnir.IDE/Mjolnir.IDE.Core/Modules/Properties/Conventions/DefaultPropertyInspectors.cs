using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Properties.Inspectors;
using Mjolnir.IDE.Sdk.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties.Conventions
{
    public static class DefaultPropertyInspectors
    {
        private static readonly List<PropertyEditorBuilder> _inspectorBuilders;
        public static IUnityContainer Container;

        public static List<PropertyEditorBuilder> InspectorBuilders
        {
            get { return _inspectorBuilders; }
        }

        static DefaultPropertyInspectors()
        {
            _inspectorBuilders = new List<PropertyEditorBuilder>
            {
                new RangePropertyEditorBuilder(Container),
                new EnumPropertyEditorBuilder(Container),

                new StandardPropertyEditorBuilder<bool, CheckBoxEditorViewModel>(Container),
                new StandardPropertyEditorBuilder<double, TextBoxEditorViewModel<double>>(Container),
                new StandardPropertyEditorBuilder<float, TextBoxEditorViewModel<float>>(Container),
                new StandardPropertyEditorBuilder<int, TextBoxEditorViewModel<int>>(Container),
                new StandardPropertyEditorBuilder<double?, TextBoxEditorViewModel<double?>>(Container),
                new StandardPropertyEditorBuilder<float?, TextBoxEditorViewModel<float?>>(Container),
                new StandardPropertyEditorBuilder<int?, TextBoxEditorViewModel<int?>>(Container),
                new StandardPropertyEditorBuilder<string, TextBoxEditorViewModel<string>>(Container)
            };
        }

        public static IEditor CreateEditor(PropertyDescriptor propertyDescriptor)
        {
            foreach (var inspectorBuilder in _inspectorBuilders)
            {
                if (inspectorBuilder.IsApplicable(propertyDescriptor))
                    return inspectorBuilder.BuildEditor(propertyDescriptor);
            }
            return null;
        }
    }
}
