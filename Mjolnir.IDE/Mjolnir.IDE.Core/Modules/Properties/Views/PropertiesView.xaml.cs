using Mjolnir.IDE.Core.Modules.Properties.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mjolnir.IDE.Core.Modules.Properties.Views
{
    /// <summary>
    /// Interaction logic for PropertiesView.xaml
    /// </summary>
    public partial class PropertiesView : UserControl, IContentView
    {
        public PropertiesView(PropertiesViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;


            // The following line simply forces Visual Studio to copy the
            // WPF Toolkit DLL to the output folder.
            _propertyGrid = null;
        }
    }
}
