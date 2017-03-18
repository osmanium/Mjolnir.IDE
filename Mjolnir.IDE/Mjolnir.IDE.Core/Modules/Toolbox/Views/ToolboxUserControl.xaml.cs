using Mjolnir.IDE.Core.Modules.Toolbox.ViewModels;
using Mjolnir.IDE.Sdk.Interfaces;
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

namespace Mjolnir.IDE.Core.Modules.Toolbox.Views
{
    /// <summary>
    /// Interaction logic for ToolboxUserControl.xaml
    /// </summary>
    public partial class ToolboxUserControl : UserControl, IContentView
    {
        public ToolboxUserControl(ToolboxViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
