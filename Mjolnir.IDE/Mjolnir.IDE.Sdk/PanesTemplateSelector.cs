using Mjolnir.IDE.Sdk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mjolnir.IDE.Sdk
{
    public class PanesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ContentViewTemplate { get; set; }

        public DataTemplate ToolViewTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ContentViewModel)
                return ContentViewTemplate;

            if (item is ToolViewModel)
                return ToolViewTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}