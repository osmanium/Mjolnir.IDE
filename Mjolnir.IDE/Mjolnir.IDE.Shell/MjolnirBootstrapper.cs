using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using System.Windows;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Shell.View;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Mjolnir.IDE.Modules.SplashScreen.ViewModel;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Modules.SplashScreen.Views;
using Mjolnir.IDE.Core;
using Mjolnir.IDE.Modules.SplashScreen;
using System.Threading;

namespace Mjolnir.IDE.Shell
{
    public class MjolnirBootstrapper : UnityBootstrapper
    {
        public bool HideSplashWindow { get; set; }

        protected override void InitializeModules()
        {
            if (!HideSplashWindow)
            {

                IModule splashModule = Container.Resolve<ISplashScreenModule>();
                splashModule.Initialize();
            }

            IModule coreModule = Container.Resolve<CoreModule>();
            coreModule.Initialize();
            

            base.InitializeModules();

            if (HideSplashWindow)
            {
                (Shell as Window).Show();
            }
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IShellView, ShellView>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISplashScreenModule, SplashScreenModule>(new ContainerControlledLifetimeManager());

            if (!Container.IsRegistered<ISplashScreenViewModel>())
                Container.RegisterType<ISplashScreenViewModel, DefaultSplashScreenViewModel>(new ContainerControlledLifetimeManager());

            if (!Container.IsRegistered<ISplashScreenView>())
                Container.RegisterType<ISplashScreenView, DefaultSplashScreenView>(new ContainerControlledLifetimeManager());

            base.ConfigureContainer();
        }

        protected override DependencyObject CreateShell()
        {
            return (DependencyObject)Container.Resolve<IShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
        }
    }
}
