using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mjolnir.IDE.Infrastructure
{
    /// <summary>
    /// Class ToolBarItemTemplateSelector
    /// </summary>
    public class ToolBarItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the button template.
        /// </summary>
        /// <value>The button template.</value>
        public DataTemplate ButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets the combo box template.
        /// </summary>
        /// <value>The combo box template.</value>
        public DataTemplate ComboBoxTemplate { get; set; }

        public DataTemplate SplitButtonTemplate { get; set; }

        /// <summary>
        /// Gets or sets the separator template.
        /// </summary>
        /// <value>The separator template.</value>
        public DataTemplate SeparatorTemplate { get; set; }

        public DataTemplate ToggleButtonTemplate { get; set; }

        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.
        /// Here, it looks at the item definition and determines if the toolbar item needs to be a button or combo box or a separator.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var toolBarItem = item as AbstractMenuItem;
            if (toolBarItem != null && !toolBarItem.IsSeparator)
            {
                if (toolBarItem.Children.Count > 0)
                    if (toolBarItem.IsSplitButton == true)
                        return SplitButtonTemplate;
                    else
                        return ComboBoxTemplate;

                if (toolBarItem.IsToggleButton)
                        return ToggleButtonTemplate;
                    else
                        return ButtonTemplate;
            }
            return SeparatorTemplate;
        }
    }
}
