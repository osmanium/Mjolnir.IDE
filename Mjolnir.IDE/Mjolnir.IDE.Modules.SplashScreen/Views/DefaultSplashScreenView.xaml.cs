﻿using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using System.Windows;

namespace Mjolnir.IDE.Modules.SplashScreen.Views
{
    /// <summary>
    /// Interaction logic for DefaultSplashScreenView.xaml
    /// </summary>
    public partial class DefaultSplashScreenView : Window, ISplashScreenView
    {
        public DefaultSplashScreenView()
        {
            InitializeComponent();
        }
    }
}
