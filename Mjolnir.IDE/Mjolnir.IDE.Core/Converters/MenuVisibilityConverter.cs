using Mjolnir.IDE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Mjolnir.IDE.Core.Converters
{
    public class MenuVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            AbstractMenuItem menu = value as AbstractMenuItem;

            if (menu == null)
                return Visibility.Hidden;

            if (menu.Command != null && menu.Command.CanExecute(null) == false && menu.HideDisabled == true)
                return Visibility.Collapsed;

            if (menu.Children.Count > 0 || menu.Command != null || menu.IsCheckable == true)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
