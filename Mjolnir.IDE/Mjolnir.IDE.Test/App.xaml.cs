using Mjolnir.IDE.Shell;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MjolnirApp
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //var container = base.Bootstrapper.Container;

            //Register own splash screen view here
            //container.RegisterType<ISplashScreenView, DefaultSplashScreenView>(new ContainerControlledLifetimeManager());

            //Register own splash screen view model here
            //container.RegisterType<ISplashScreenViewModel, DefaultSplashScreenViewModel>(new ContainerControlledLifetimeManager());

            base.OnStartup(e);
        }
    }
}
