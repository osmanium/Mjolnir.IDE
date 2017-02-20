using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Shell.Themes
{
    /// <summary>
    /// Class VS2010
    /// </summary>
    public sealed class VS2010 : ITheme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VS2010"/> class.
        /// </summary>
        public VS2010()
        {
            UriList = new List<Uri>
                          {
                              new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2010;component/Theme.xaml"),
                              new Uri("pack://application:,,,/Wide;component/Interfaces/Styles/VS2010/Theme.xaml")
                          };
        }

        #region ITheme Members

        /// <summary>
        /// Lists of valid URIs which will be loaded in the theme dictionary
        /// </summary>
        /// <value>The URI list.</value>
        public IList<Uri> UriList { get; private set; }

        /// <summary>
        /// The name of the theme - "VS2010"
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return "VS2010"; }
        }

        #endregion
    }
}