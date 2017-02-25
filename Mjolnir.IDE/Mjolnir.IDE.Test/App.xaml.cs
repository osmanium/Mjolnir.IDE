using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Shell;
using Mjolnir.IDE.Test.TextDocument;
using Mjolnir.IDE.Test.TextDocument.Model;
using Mjolnir.IDE.Test.TextDocument.View;
using Mjolnir.IDE.Test.TextDocument.ViewModel;
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
        private IEventAggregator _eventAggregator;

        private string _applicationName;
        public string ApplicationName { get { return _applicationName; } set { _applicationName = value; } }

        private ImageSource _applicationIconSource;
        public ImageSource ApplicationIconSource { get { return _applicationIconSource; } set { _applicationIconSource = value; } }

        public App()
        {
            //Set application specific information
            //_applicationName = "Test Application";
            //_applicationIconSource = new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/mjolnir_override.png"));

            base.ApplicationDefinition = this;
        }

        public void InitalizeIDE()
        {
            _container = Bootstrapper.Container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
        }

        public void LoadCommands()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Commands..." });
            var manager = _container.Resolve<ICommandManager>();

            var openCommand = new DelegateCommand(OpenModule);
            var exitCommand = new DelegateCommand(CloseCommandExecute);
            var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            var saveAsCommand = new DelegateCommand(SaveAsDocument, CanExecuteSaveAsDocument);
            var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);
            var loggerCommand = new DelegateCommand(ToggleOutput);


            manager.RegisterCommand("OPEN", openCommand);
            manager.RegisterCommand("SAVE", saveCommand);
            manager.RegisterCommand("SAVEAS", saveAsCommand);
            manager.RegisterCommand("EXIT", exitCommand);
            manager.RegisterCommand("LOGSHOW", loggerCommand);
            manager.RegisterCommand("THEMECHANGE", themeCommand);
        }

        public void RegisterTypes()
        {
            //Register own splash screen view here
            //container.RegisterType<ISplashScreenView, CustomSplashScreenView>(new ContainerControlledLifetimeManager());

            //Register own splash screen view model here
            //container.RegisterType<ISplashScreenViewModel, CustomSplashScreenViewModel>(new ContainerControlledLifetimeManager());

            //Register Workspace
            //_container.RegisterType<AbstractWorkspace, Workspace>(new ContainerControlledLifetimeManager());

            _container.RegisterType<TextViewModel>();
            _container.RegisterType<TextModel>();
            _container.RegisterType<TextView>();
            _container.RegisterType<AllFileHandler>();

            //Register a default file opener
            var registry = _container.Resolve<IContentHandlerRegistry>();
            registry.Register(_container.Resolve<AllFileHandler>());
        }

        public void OnIDELoaded()
        {

        }

        public void LoadMenus()
        {
            var menuService = _container.Resolve<IMenuService>();
            var manager = _container.Resolve<ICommandManager>();
            var settingsManager = _container.Resolve<ISettingsManager>();
            var themeSettings = _container.Resolve<IThemeSettings>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Output");

            menuService.Add(new MenuItemViewModel("_File", 1));
            menuService.Add(new MenuItemViewModel("_Edit", 2));
            menuService.Add(new MenuItemViewModel("_View", 3));
            menuService.Add(new MenuItemViewModel("_Tools", 4));
            menuService.Add(new MenuItemViewModel("_Help", 5));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_New", 3,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/NewRequest_8796.png")),
                                       manager.GetCommand("NEW"),
                                       new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_Open", 4,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/OpenFileDialog_692.png")),
                                       manager.GetCommand("OPEN"),
                                       new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            menuService.Get("_File").Add(new MenuItemViewModel("_Save", 5,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
                                                               manager.GetCommand("SAVE"),
                                                               new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));

            //menuService.Get("_File").Add(new SaveAsMenuItemViewModel("Save As..", 6,
            //                                       new BitmapImage(
            //                                           new Uri(
            //                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
            //                                       manager.GetCommand("SAVEAS"), null, false, false, _container));

            menuService.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));

            //menuService.Get("_File").Add(recentFiles.RecentMenu);

            menuService.Get("_File").Add(new MenuItemViewModel("E_xit", 101, null, manager.GetCommand("EXIT"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt + F4")));


            menuService.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Undo_16x.png")),
                                                               ApplicationCommands.Undo));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Redo_16x.png")),
                                                               ApplicationCommands.Redo));
            menuService.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Cut", 20,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Cut_6523.png")),
                                                               ApplicationCommands.Cut));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Copy", 21,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Copy_6524.png")),
                                                               ApplicationCommands.Copy));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Paste_6520.png")),
                                                               ApplicationCommands.Paste));

            

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

            
            menuService.Get("_Tools").Add(new MenuItemViewModel("Settings", 1, null, settingsManager.SettingsCommand));


        }

        public void LoadSettings()
        {

        }

        public void LoadTheme()
        {
            //TODO : Load settings before theme
            //new SplashMessageUpdateEvent().Publish(new SplashMessageUpdateEvent() { Message = "Themes.." });
            //var manager = _container.Resolve<IThemeManager>();
            //var themeSettings = _container.Resolve<IThemeSettings>();
            //var win = _container.Resolve<IShell>() as Window;
            //manager.AddTheme(new LightTheme());
            //manager.AddTheme(new DarkTheme());
            //win.Dispatcher.InvokeAsync(() => manager.SetCurrent(themeSettings.SelectedTheme));
        }

        public void LoadToolbar()
        {
            var toolbarService = _container.Resolve<IToolbarService>();
            var menuService = _container.Resolve<IMenuService>();
            var manager = _container.Resolve<ICommandManager>();

            toolbarService.Add(new ToolbarViewModel("Standard", 1) { Band = 1, BandIndex = 1 });
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_New"));
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Open"));

            toolbarService.Add(new ToolbarViewModel("Edit", 1) { Band = 1, BandIndex = 2 });
            toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Undo"));
            toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Redo"));
            toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Cut"));
            toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Copy"));
            toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Paste"));

            //toolbarService.Add(new ToolbarViewModel("Debug", 1) { Band = 1, BandIndex = 3 });
            //toolbarService.Get("Debug").Add(new MenuItemViewModel("Debug", 1, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Play.png"))));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with Chrome", 1, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Play.png")), manager.GetCommand("OPEN")));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with FireFox", 2, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Play.png")), manager.GetCommand("OPEN")));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with Explorer", 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Play.png")), manager.GetCommand("OPEN")));

            menuService.Get("_Tools").Add(toolbarService.RightClickMenu);

            //Initiate the position settings changes for toolbar
            _container.Resolve<IToolbarPositionSettings>();
        }
        
        public void LoadModules()
        {

        }
        
        public bool onIDEClosing()
        {
            return true;
        }

        public void onIDEClosed()
        {

        }




        #region Commands Methods

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
        
        private void OpenModule()
        {
            var service = _container.Resolve<IOpenDocumentService>();
            service.Open();
        }
        
        private bool CanExecuteSaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            if (workspace.ActiveDocument != null)
            {
                return workspace.ActiveDocument.Model.IsDirty;
            }
            return false;
        }

        private bool CanExecuteSaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            return (workspace.ActiveDocument != null);
        }

        private void SaveDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument);
            manager.Refresh();
        }

        private void SaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            if (workspace.ActiveDocument != null)
            {
                workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument, true);
                manager.Refresh();
            }
        }

        private void CloseCommandExecute()
        {
            IShellView shell = _container.Resolve<IShellView>();
            shell.Close();
        }

        #endregion
    }
}
