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
using Prism.Mvvm;
using Mjolnir.IDE.Shell.ViewModel;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Shell.Themes;

namespace Mjolnir.IDE.Shell
{
    public class MjolnirBootstrapper : UnityBootstrapper
    {
        public bool HideSplashWindow { get; set; }

        private readonly IApplicationDefinition _applicationDefinition;

        public MjolnirBootstrapper(IApplicationDefinition ApplicationDefinition)
        {
            _applicationDefinition = ApplicationDefinition;
        }

        protected override void InitializeModules()
        {
            if (!HideSplashWindow)
            {
                IModule splashModule = Container.Resolve<ISplashScreenModule>();
                splashModule.Initialize();
            }

            
            CoreModule coreModule = Container.Resolve<CoreModule>();
            coreModule.Initialize();

            //TODO : Improve here
            var shellViewModel = Container.Resolve<IShellView>();
            (shellViewModel.DataContext as ShellViewModel).Workspace = Container.Resolve<AbstractWorkspace>();

            
        
            var manager = Container.Resolve<IThemeManager>();
            var win = Container.Resolve<IShellView>() as Window;
            manager.AddTheme(new LightTheme());
            manager.AddTheme(new DarkTheme());

            win.Dispatcher.InvokeAsync(() => manager.SetCurrent("Light"));
            

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

            Container.RegisterInstance<IApplicationDefinition>(_applicationDefinition, new ContainerControlledLifetimeManager());

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

        protected override void ConfigureViewModelLocator()
        {
            BindViewModelToView<ShellViewModel, ShellView>();
        }

        public void BindViewModelToView<TViewModel, TView>()
        {
            ViewModelLocationProvider.Register(typeof(TView).ToString(), () => Container.Resolve<TViewModel>());
        }
    }
}
