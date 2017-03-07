using Mjolnir.IDE.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Core.Modules.ErrorList.Converters
{
    public class ErrorListItemTypeToImageConverter : IValueConverter
    {
        public string ErrorImageSource { get; set; }
        public string MessageImageSource { get; set; }
        public string WarningImageSource { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();

            var path = string.Empty;

            switch ((ErrorListItemType)value)
            {
                case ErrorListItemType.Error:
                    path = ErrorImageSource;
                    break;
                case ErrorListItemType.Warning:
                    path = WarningImageSource;
                    break;
                case ErrorListItemType.Message:
                    path = MessageImageSource;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }

            bmp.UriSource = new Uri(path, UriKind.Relative);
            bmp.EndInit();
            
            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}