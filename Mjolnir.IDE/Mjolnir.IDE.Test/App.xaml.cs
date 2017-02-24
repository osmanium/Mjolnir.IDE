using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Shell;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Test
{
    public partial class App : MjolnirApp, IApplicationDefinition
    {
        private IUnityContainer _container;

        private string _applicationName;
        public string ApplicationName { get { return _applicationName; } }

        private ImageSource _applicationIconSource;
        public ImageSource ApplicationIconSource { get { return _applicationIconSource; } }

        public App()
        {
            //Set application specific information
            //_applicationName = "test test";
            //_applicationIconSource = new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/mjolnir_override.png"));

            base.ApplicationDefinition = this;
        }

        public void InitalizeIDE()
        {
            _container = Bootstrapper.Container;
        }

        public void RegisterTypes()
        {
            //Register own splash screen view here
            //container.RegisterType<ISplashScreenView, CustomSplashScreenView>(new ContainerControlledLifetimeManager());

            //Register own splash screen view model here
            //container.RegisterType<ISplashScreenViewModel, CustomSplashScreenViewModel>(new ContainerControlledLifetimeManager());
        }

        public void OnIDELoaded()
        {

        }

        public void LoadMenus()
        {
            var menuService = _container.Resolve<IMenuService>();
            //var manager = _container.Resolve<ICommandManager>();
            var settingsManager = _container.Resolve<ISettingsManager>();
            var themeSettings = _container.Resolve<IThemeSettings>();
            var recentFiles = _container.Resolve<IRecentViewSettings>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Output");

            menuService.Add(new MenuItemViewModel("_File", 1));

            menuService.Add(new MenuItemViewModel("_View", 3));

            if (output != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Output", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Undo_16x.png")),
                                    new DelegateCommand(ToggleOutput) { IsActive = false }));

            menuService.Get("_View").Add(new MenuItemViewModel("Themes", 1));

            //Set the checkmark of the theme menu's based on which is currently selected
            menuService.Get("_View").Get("Themes")
                                    .Add(new MenuItemViewModel("Dark", 1,
                                             null,
                                             new DelegateCommand<string>(ThemeChangeCommand))
                                    {
                                        IsCheckable = true,
                                        IsChecked = (themeSettings.SelectedTheme == "Dark"),
                                        CommandParameter = "Dark"
                                    });
            menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Light", 2, null,
                                                                             new DelegateCommand<string>(ThemeChangeCommand))
            {
                IsCheckable = true,
                IsChecked = (themeSettings.SelectedTheme == "Light"),
                CommandParameter = "Light"
            });

            menuService.Add(new MenuItemViewModel("_Tools", 4));
            menuService.Get("_Tools").Add(new MenuItemViewModel("Settings", 1, null, settingsManager.SettingsCommand));

            menuService.Add(new MenuItemViewModel("_Help", 4));
        }

        public void LoadSettings()
        {

        }

        public void LoadTheme()
        {

        }

        public void LoadToolbar()
        {

        }
        
        public void LoadModules()
        {

        }
        
        public void onIDEClosing()
        {

        }

        public void onIDEClosed()
        {

        }



        private void ThemeChangeCommand(string s)
        {
            var manager = _container.Resolve<IThemeManager>();
            var menuService = _container.Resolve<IMenuService>();
            var win = _container.Resolve<IShellView>() as Window;
            var eventAggregater = _container.Resolve<IEventAggregator>();

            MenuItemViewModel mvm =
                menuService.Get("_View").Get("Themes").Get(manager.CurrentTheme.Name) as MenuItemViewModel;

            if (manager.CurrentTheme.Name != s)
            {
                if (mvm != null)
                    mvm.IsChecked = false;
                win.Dispatcher.InvokeAsync(() => manager.SetCurrent(s));
            }
            else
            {
                if (mvm != null)
                    mvm.IsChecked = true;
            }
        }

        private void ToggleOutput()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Output");
            if (output != null)
            {
                output.IsVisible = !output.IsVisible;
                var mi = menuService.Get("_View").Get("_Output") as MenuItemViewModel;
                mi.IsChecked = output.IsVisible;
            }
        }

    }
}
