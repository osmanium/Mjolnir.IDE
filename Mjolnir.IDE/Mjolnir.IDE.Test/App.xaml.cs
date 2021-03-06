﻿using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core;
using Mjolnir.IDE.Core.Modules.ErrorList;
using Mjolnir.IDE.Core.Modules.ErrorList.Events;
using Mjolnir.IDE.Core.Modules.Settings;
using Mjolnir.IDE.Core.Modules.Toolbox;
using Mjolnir.IDE.Core.Services;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Interfaces.Settings;
using Mjolnir.IDE.Sdk.Interfaces.ViewModels;
using Mjolnir.IDE.Sdk.Interfaces.Views;
using Mjolnir.IDE.Sdk.Util;
using Mjolnir.IDE.Sdk.ViewModels;
using Mjolnir.IDE.Test.ProjectExplorer;
using Mjolnir.IDE.Test.ProjectExplorer.Interfaces;
using Mjolnir.IDE.Test.TextDocument;
using Mjolnir.IDE.Test.TextDocument.Model;
using Mjolnir.IDE.Test.TextDocument.Toolbox;
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
    public partial class App : MjolnirApp
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public App()
        {
            //Set application specific information
            //_applicationName = "Test Application";
            //_applicationIconSource = new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/mjolnir_override.png"));
        }


        public override void InitalizeIDE()
        {
            _eventAggregator = _container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<IDELoadedEvent>().Subscribe(App_OnIDELoaded);
            _eventAggregator.GetEvent<IDEClosingEvent>().Subscribe(App_OnIDEClosing);
            _eventAggregator.GetEvent<IDEClosedEvent>().Subscribe(App_OnIDEClosed);
        }

        public override void LoadCommands()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Commands..." });
            var manager = _container.Resolve<ICommandManager>();

            var openCommand = new DelegateCommand(OpenModule);
            var exitCommand = new DelegateCommand(CloseCommandExecute);
            var saveCommand = new DelegateCommand(SaveDocument, CanExecuteSaveDocument);
            var saveAllCommand = new DelegateCommand(SaveAllDocuments, CanExecuteSaveAllDocument);
            var saveAsCommand = new DelegateCommand(SaveAsDocument, CanExecuteSaveAsDocument);
            var themeCommand = new DelegateCommand<string>(ThemeChangeCommand);
            var loggerCommand = new DelegateCommand(ToggleOutput);
            var toolboxCommand = new DelegateCommand(ToggleToolbox);
            var solutionExplorerCommand = new DelegateCommand(ToggleProjectExplorer);


            manager.RegisterCommand(CommandManagerConstants.OpenCommand, openCommand);
            manager.RegisterCommand(CommandManagerConstants.SaveCommand, saveCommand);
            manager.RegisterCommand(CommandManagerConstants.SaveAllCommand, saveAllCommand);
            manager.RegisterCommand(CommandManagerConstants.SaveAsCommand, saveAsCommand);
            manager.RegisterCommand(CommandManagerConstants.ExitCommand, exitCommand);
            manager.RegisterCommand(CommandManagerConstants.LogShowCommand, loggerCommand);
            manager.RegisterCommand(CommandManagerConstants.ThemeChangeCommand, themeCommand);
            manager.RegisterCommand(CommandManagerConstants.ToggleToolboxCommand, toolboxCommand);
            manager.RegisterCommand(CommandManagerConstants.ToggleProjectExplorerCommand, solutionExplorerCommand);
        }

        public override void RegisterTypes()
        {
            _container = Bootstrapper.Container;

            //Register own splash screen view here
            //container.RegisterType<ISplashScreenView, CustomSplashScreenView>(new ContainerControlledLifetimeManager());

            //Register own splash screen view model here
            //container.RegisterType<ISplashScreenViewModel, CustomSplashScreenViewModel>(new ContainerControlledLifetimeManager());

            //Register Workspace
            _container.RegisterType<DefaultWorkspace, Workspace>(new ContainerControlledLifetimeManager());

            _container.RegisterType<TextViewModel>();
            _container.RegisterType<TextModel>();
            _container.RegisterType<TextView>();
            _container.RegisterType<AllFileHandler>();

            //Register a default file opener
            var registry = _container.Resolve<IContentHandlerRegistry>();
            registry.Register(_container.Resolve<AllFileHandler>());
        }



        public override void LoadMenus()
        {
            var menuService = _container.Resolve<IMenuService>();
            var manager = _container.Resolve<ICommandManager>();
            var settingsManager = _container.Resolve<ISettingsManager>();
            var themeSettings = _container.Resolve<IThemeSettings>();
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();

            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Output");
            ToolViewModel error = workspace.Tools.First(f => f.ContentId == "Error");
            ToolViewModel toolbox = workspace.Tools.First(f => f.ContentId == "Toolbox");
            ToolViewModel projectExplorer = workspace.Tools.First(f => f.ContentId == "Project Explorer");
            ToolViewModel properties = workspace.Tools.First(f => f.ContentId == "Properties");


            menuService.Add(new MenuItemViewModel("_File", "_File", 1));
            menuService.Add(new MenuItemViewModel("_Edit", "_Edit", 2));
            menuService.Add(new MenuItemViewModel("_View", "_View", 3));
            menuService.Add(new MenuItemViewModel("_Tools", "_Tools", 4));
            menuService.Add(new MenuItemViewModel("_Help", "_Help", 5));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_New", "_New", 3,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/NewRequest_8796.png")),
                                       manager.GetCommand(CommandManagerConstants.NewItem),
                                       new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_Open", "_Open", 4,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/OpenFileDialog_692.png")),
                                       manager.GetCommand("OPEN"),
                                       new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));

            menuService.Get("_File").Add(new MenuItemViewModel("_Save", "_Save", 5,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
                                                               manager.GetCommand("SAVE"),
                                                               new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));

            menuService.Get("_File").Add(new MenuItemViewModel("_Save All", "_Save All", 6,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Saveall_6518.png")),
                                                               manager.GetCommand("SAVEALL"),
                                                               new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl + A")));


            //menuService.Get("_File").Add(new SaveAsMenuItemViewModel("Save As..", 6,
            //                                       new BitmapImage(
            //                                           new Uri(
            //                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
            //                                       manager.GetCommand("SAVEAS"), null, false, false, _container));

            menuService.Get("_File").Add(new MenuItemViewModel("Close", "Close", 8, null, manager.GetCommand("CLOSE"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));

            //menuService.Get("_File").Add(recentFiles.RecentMenu);

            menuService.Get("_File").Add(new MenuItemViewModel("E_xit", "E_xit", 101, null, manager.GetCommand("EXIT"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt + F4")));


            menuService.Get("_Edit").Add(new MenuItemViewModel("_Undo", "_Undo", 1,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Undo_16x.png")),
                                                               ApplicationCommands.Undo));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Redo", "_Redo", 2,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Redo_16x.png")),
                                                               ApplicationCommands.Redo));
            menuService.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Cut", "Cut", 20,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Cut_6523.png")),
                                                               ApplicationCommands.Cut));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Copy", "Copy", 21,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Copy_6524.png")),
                                                               ApplicationCommands.Copy));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Paste", "_Paste", 22,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Paste_6520.png")),
                                                               ApplicationCommands.Paste));



            if (output != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Output", "_Output", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Output_16xLG.png")),
                                    new DelegateCommand(ToggleOutput) { IsActive = false }));

            if (error != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Error", "_Error", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Error_6206.png")),
                                    new DelegateCommand(ErrorOutput) { IsActive = false }));


            if (toolbox != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Toolbox", "_Toolbox", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/toolbox_16xLG.png")),
                                    new DelegateCommand(ToggleToolbox) { IsActive = false }));

            if (projectExplorer != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Project_Explorer", "_Project_Explorer", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/toolbox_16xLG.png")),
                                    new DelegateCommand(ToggleProjectExplorer) { IsActive = false }));

            if (properties != null)
                menuService.Get("_View")
                           .Add(new MenuItemViewModel("_Properties", "_Properties", 1,
                                    new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/toolbox_16xLG.png")),
                                    new DelegateCommand(ToggleProperties) { IsActive = false }));



            menuService.Get("_View").Add(new MenuItemViewModel("Themes", "Themes", 1));

            //Set the checkmark of the theme menu's based on which is currently selected
            menuService.Get("_View").Get("Themes")
                                    .Add(new MenuItemViewModel("Dark", "Dark", 1, null,
                                             new DelegateCommand<string>(ThemeChangeCommand))
                                    {
                                        IsCheckable = true,
                                        IsChecked = (themeSettings.SelectedTheme == "Dark"),
                                        CommandParameter = "Dark"
                                    });

            menuService.Get("_View").Get("Themes")
                                    .Add(new MenuItemViewModel("Light", "Light", 2, null,
                                            new DelegateCommand<string>(ThemeChangeCommand))
                                    {
                                        IsCheckable = true,
                                        IsChecked = (themeSettings.SelectedTheme == "Light"),
                                        CommandParameter = "Light"
                                    });


            menuService.Get("_Tools").Add(new MenuItemViewModel("Settings", "Settings", 1, null, settingsManager.SettingsCommand));


        }

        public override void LoadSettings()
        {
            ISettingsManager manager = _container.Resolve<ISettingsManager>();
            manager.Add(new MjolnirSettingsItem("Mjolnir Settings", 1, null));
            manager.Get("Mjolnir Settings").Add(new MjolnirSettingsItem("General", 1, MjolnirTestSettings.Default));
        }

        public override void LoadTheme()
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

        public override void LoadToolbar()
        {
            var toolbarService = _container.Resolve<IShellToolbarService>();
            var menuService = _container.Resolve<IMenuService>();
            var manager = _container.Resolve<ICommandManager>();

            toolbarService.Add(new ToolbarViewModel("Standard", "Standard", 1) { Band = 1, BandIndex = 1 });
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_New"));
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Open"));
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Save"));
            toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Save All"));



            toolbarService.Add(new ToolbarViewModel("Edit", "Edit", 1) { Band = 1, BandIndex = 2 });
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

            var projectExplorerToolbarService = _container.Resolve<IProjectExplorerToolboxToolbarService>();


            projectExplorerToolbarService.Add(new ToolbarViewModel("Standard", "Standard", 1) { Band = 1, BandIndex = 1 });
            projectExplorerToolbarService.Get("Standard").Add(menuService.Get("_File").Get("_New"));
            projectExplorerToolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Open"));


        }

        public override void LoadModules()
        {
            ErrorListModule errorModule = _container.Resolve<ErrorListModule>();
            errorModule.Initialize();


            ToolboxModule toolboxModule = _container.Resolve<ToolboxModule>();
            toolboxModule.Initialize();


            //Load properties

            //Load project explorer
            ProjectExplorerModule projectExplorerModule = _container.Resolve<ProjectExplorerModule>();
            projectExplorerModule.Initialize();
        }


        private void App_OnIDEClosed()
        {

        }

        private void App_OnIDEClosing(IDEClosingEvent e)
        {
            var shell = _container.Resolve<IShellView>();
            shell.SaveLayout();
        }

        private void App_OnIDELoaded()
        {
            //Log error
            var errorService = _container.Resolve<IErrorService>();
            errorService.LogError(new ErrorListItem(
                itemType: Sdk.Enums.ErrorListItemType.Error,
                number: 1,
                description: "Test description",
                path: "Path",
                line: 1,
                column: 1));


            errorService.LogError(new ErrorListItem(
                itemType: Sdk.Enums.ErrorListItemType.Warning,
                number: 1,
                description: "Test description",
                path: "Path",
                line: 2,
                column: 1));

            errorService.LogError(new ErrorListItem(
                itemType: Sdk.Enums.ErrorListItemType.Message,
                number: 1,
                description: "Test description",
                path: "Path",
                line: 3,
                column: 1));


            //Update statusbar values
            var _statusBar = _container.Resolve<IStatusbarService>();
            _statusBar.CharPosition = 3;
            _statusBar.ColPosition = 4;
            _statusBar.LineNumber = 5;
            _statusBar.Progress(true, 50, 100);
            _statusBar.Text = "Building...";
            _statusBar.InsertMode = true;


            //Dynamically toolbar item can be added
            var _outputToolboxService = _container.Resolve<IOutputToolboxToolbarService>();
            var manager = _container.Resolve<ICommandManager>();
            _outputToolboxService.Get("Standard").Add(new MenuItemViewModel("_Clear All 2", "Clear All 2", 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Clearwindowcontent_6304.png")), manager.GetCommand("CLEAROUTPUT"), null, false, false, null, false));


            //Create new output service and new log in it
            var _outputService = _container.Resolve<IOutputService>();
            _outputService.AddOutputSource("TestOutputSource");
            _outputService.LogOutput(new LogOutputItem("TestOutputSource message", OutputCategory.Info, OutputPriority.High, "TestOutputSource"));


            //Add toolbox items
            var _toolboxService = _container.Resolve<IToolboxService>();
            _toolboxService.AddToolboxItems(typeof(TextViewModel).Name, new List<ToolboxItem>()
            {
                new ToolboxItem()
                {
                    Category = "Manually Added Items",
                    DocumentType = typeof(TextViewModel),
                    ItemType = typeof(TestToolboxItem),
                    Name = "Manual Item 1"
                },
                new ToolboxItem()
                {
                    Category = "Manually Added Items",
                    DocumentType = typeof(TextViewModel),
                    ItemType = typeof(TestToolboxItem),
                    Name = "Manual Item 2"
                },
                new ToolboxItem()
                {
                    Category = "Manually Added Items",
                    DocumentType = typeof(TextViewModel),
                    ItemType = typeof(TestToolboxItem),
                    Name = "Manual Item 3"
                }
            });

            //Remove toolbox item
            _toolboxService.RemoveToolboxItems(typeof(TextViewModel).Name, new List<ToolboxItem>()
            {
                new ToolboxItem()
                {
                    Category = "Manually Added Items",
                    DocumentType = typeof(TextViewModel),
                    ItemType = typeof(TestToolboxItem),
                    Name = "Manual Item 3"
                }
            });


            //Update property grid selected object
            var _propertyGrid = _container.Resolve<IPropertyGrid>();
            _propertyGrid.SelectedObject = _statusBar;

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
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Output");
            if (output != null)
            {
                output.IsVisible = !output.IsVisible;
                var mi = menuService.Get("_View").Get("_Output") as MenuItemViewModel;
                mi.IsChecked = output.IsVisible;
            }
        }
        private void ErrorOutput()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel output = workspace.Tools.First(f => f.ContentId == "Error");
            if (output != null)
            {
                output.IsVisible = !output.IsVisible;
                var mi = menuService.Get("_View").Get("_Error") as MenuItemViewModel;
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
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            if (workspace.ActiveDocument != null)
            {
                return workspace.ActiveDocument.Model.IsDirty;
            }
            return false;
        }

        private bool CanExecuteSaveAllDocument()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            if (workspace.Documents != null && workspace.Documents.Any())
            {
                return true;
            }
            return false;
        }

        private bool CanExecuteSaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            return (workspace.ActiveDocument != null);
        }

        private void SaveDocument()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument);
            manager.Refresh();
        }

        private void SaveAllDocuments()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            ICommandManager manager = _container.Resolve<ICommandManager>();
            workspace.Documents.ToList().ForEach(f =>
            {
                f.Handler.SaveContent(f);
            });
            manager.Refresh();
        }

        private void SaveAsDocument()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
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

        private void ToggleToolbox()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel toolbox = workspace.Tools.First(f => f.ContentId == "Toolbox");
            if (toolbox != null)
            {
                toolbox.IsVisible = !toolbox.IsVisible;
                var mi = menuService.Get("_View").Get("_Toolbox") as MenuItemViewModel;
                mi.IsChecked = toolbox.IsVisible;
            }
        }

        private void ToggleProjectExplorer()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel toolbox = workspace.Tools.First(f => f.ContentId == "Project Explorer");
            if (toolbox != null)
            {
                toolbox.IsVisible = !toolbox.IsVisible;
                var mi = menuService.Get("_View").Get("_Toolbox") as MenuItemViewModel;
                mi.IsChecked = toolbox.IsVisible;
            }
        }

        private void ToggleProperties()
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            var menuService = _container.Resolve<IMenuService>();
            ToolViewModel toolbox = workspace.Tools.First(f => f.ContentId == "Properties");
            if (toolbox != null)
            {
                toolbox.IsVisible = !toolbox.IsVisible;
                var mi = menuService.Get("_View").Get("_Toolbox") as MenuItemViewModel;
                mi.IsChecked = toolbox.IsVisible;
            }
        }



        #endregion

    }
}
