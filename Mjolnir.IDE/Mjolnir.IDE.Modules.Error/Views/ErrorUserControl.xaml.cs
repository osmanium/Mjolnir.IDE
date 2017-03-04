using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Modules.Error.ViewModels;
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

namespace Mjolnir.IDE.Modules.Error.Views
{
    /// <summary>
    /// Interaction logic for ErrorUserControl.xaml
    /// </summary>
    public partial class ErrorUserControl : UserControl, IContentView
    {
        public ErrorUserControl(ErrorViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
