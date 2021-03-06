﻿using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Modularity;
using System.Windows;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Views;
using Mjolnir.IDE.Sdk.Interfaces.ViewModels;
using Mjolnir.IDE.Core;
using System.Threading;
using Prism.Mvvm;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Core.Modules.Shell.ViewModels;
using Mjolnir.IDE.Core.Themes;
using Mjolnir.IDE.Core.Modules.Shell.Views;
using Mjolnir.IDE.Core.Modules.SplashScreen;
using Mjolnir.IDE.Core.Modules.SplashScreen.ViewModels;
using Mjolnir.IDE.Core.Modules.SplashScreen.Views;

namespace Mjolnir.IDE.Core
{
    public class MjolnirBootstrapper : UnityBootstrapper
    {
        public bool HideSplashWindow { get; set; }

        private readonly MjolnirApp _app;

        public MjolnirBootstrapper(MjolnirApp app)
        {
            _app = app;
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

            
            var shellViewModel = Container.Resolve<IShellView>();
            (shellViewModel.DataContext as ShellViewModel).Workspace = Container.Resolve<DefaultWorkspace>();

            
        
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
            {
                Container.RegisterType<ISplashScreenViewModel, DefaultSplashScreenViewModel>(new ContainerControlledLifetimeManager());

            }


            if (!Container.IsRegistered<ISplashScreenView>())
            {
                Container.RegisterType<ISplashScreenView, DefaultSplashScreenView>(new ContainerControlledLifetimeManager());
            }

            Container.RegisterInstance<MjolnirApp>(_app, new ContainerControlledLifetimeManager());

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
