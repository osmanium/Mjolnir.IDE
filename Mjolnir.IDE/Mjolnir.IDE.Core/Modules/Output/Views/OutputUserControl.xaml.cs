using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Core.Modules.Output.ViewModels;
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

namespace Mjolnir.IDE.Core.Modules.Output.Views
{
    /// <summary>
    /// Interaction logic for OutputUserControl.xaml
    /// </summary>
    public partial class OutputUserControl : UserControl, IContentView
    {
        //TODO : instead of mode, it should be view model, check here
        public OutputUserControl(OutputViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            //TODO : put scrollbar in xaml

        }
    }
}
