﻿using Mjolnir.IDE.Modules.Output.ViewModels;
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

namespace Mjolnir.IDE.Modules.Output.Views
{
    /// <summary>
    /// Interaction logic for OutputUserControl.xaml
    /// </summary>
    public partial class OutputUserControl : UserControl
    {
        public OutputUserControl(OutputViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
