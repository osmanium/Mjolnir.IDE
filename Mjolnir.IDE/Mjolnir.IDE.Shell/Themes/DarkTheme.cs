using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Shell.Themes
{
    /// <summary>
    /// Class DarkTheme
    /// </summary>
    public sealed class DarkTheme : ITheme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DarkTheme"/> class.
        /// </summary>
        public DarkTheme()
        {
            UriList = new List<Uri>
                          {
                              new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml"),
                              new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml"),
                              new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Colors.xaml"),
                              new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml"),
                              new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml"),
                              new Uri("pack://application:,,,/Mjolnir.IDE.Shell;component/Styles/VS2013/DarkColors.xaml"),
                              new Uri("pack://application:,,,/Mjolnir.IDE.Shell;component/Styles/VS2013/DarkTheme.xaml"),
                              new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/DarkTheme.xaml"),
                              new Uri("pack://application:,,,/Mjolnir.IDE.Shell;component/Styles/VS2013/Menu.xaml")
                          };
        }

        #region ITheme Members

        /// <summary>
        /// Lists of valid URIs which will be loaded in the theme dictionary
        /// </summary>
        /// <value>The URI list.</value>
        public IList<Uri> UriList { get; internal set; }

        /// <summary>
        /// The name of the theme - "Dark"
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "Dark"; }
        }

        #endregion
    }
}