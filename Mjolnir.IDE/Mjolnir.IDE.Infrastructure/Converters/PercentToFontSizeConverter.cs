using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mjolnir.IDE.Infrastructure.Converters
{
    public class PercentToFontSizeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //For now lets assume 12.00 to be 100%
            var fsize = value as double?;
            if (fsize != null)
            {
                return ((fsize / 12.00) * 100) + " %";
            }
            return "100 %";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double rValue = 12.0;
            if (value != null)
            {
                var final = value as string;
                final = final.Replace("%", "");
                if (double.TryParse(final, out rValue))
                {
                    rValue = (rValue / 100.0) * 12;
                }
                else
                {
                    rValue = 12.0;
                }
            }
            return rValue;
        }

        #endregion
    }
}
