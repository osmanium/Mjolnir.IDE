using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.ErrorList;
using Mjolnir.IDE.Core.Modules.Output;
using Mjolnir.IDE.Core.Modules.Properties;
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
using Mjolnir.IDE.Sdk.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Core
{
    public class CoreModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private IOutputService _outputService;
        private readonly MjolnirApp _app;

        public CoreModule(IUnityContainer container,
                          IEventAggregator eventAggregator,
                          MjolnirApp app)
        {
            this._container = container;
            this._eventAggregator = eventAggregator;
            this._app = app;
        }


        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Components..." });

            _container.RegisterType<IThemeSettings, ThemeSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRecentViewSettings, RecentViewSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWindowPositionSettings, WindowPositionSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IToolbarPositionSettings, ToolbarPositionSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICommandManager, Mjolnir.IDE.Core.Services.CommandManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IContentHandlerRegistry, ContentHandlerRegistry>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IStatusbarService, MjolnirStatusbarViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IThemeManager, ThemeManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IShellToolbarService, ShellToolbarService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMenuService, MenuItemViewModel>(new ContainerControlledLifetimeManager(),
                                                                     new InjectionConstructor(
                                                                         new InjectionParameter(typeof(string),
                                                                                                "$MAIN$"),
                                                                         new InjectionParameter(typeof(string),
                                                                                                "$MAIN$"),
                                                                         new InjectionParameter(typeof(int), 1),
                                                                         new InjectionParameter(
                                                                             typeof(ImageSource), null),
                                                                         new InjectionParameter(typeof(ICommand),
                                                                                                null),
                                                                         new InjectionParameter(
                                                                             typeof(KeyGesture), null),
                                                                         new InjectionParameter(typeof(bool), false),
                                                                         new InjectionParameter(typeof(bool), false),
                                                                         new InjectionParameter(
                                                                             typeof(IUnityContainer), _container),
                                                                         new InjectionParameter(typeof(bool), false),
                                                                         new InjectionParameter(typeof(bool), false)));
            _container.RegisterType<ToolbarViewModel>(
                new InjectionConstructor(new InjectionParameter(typeof(string), "$MAIN$"),
                                         new InjectionParameter(typeof(string), "$MAIN$"),
                                         new InjectionParameter(typeof(int), 1),
                                         new InjectionParameter(typeof(ImageSource), null),
                                         new InjectionParameter(typeof(ICommand), null),
                                         new InjectionParameter(typeof(bool), false),
                                         new InjectionParameter(typeof(IUnityContainer), _container)));

            _container.RegisterType<ISettingsManager, SettingsManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IOpenDocumentService, OpenDocumentService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMessageDialogService, DefaultMessageDialogService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IFileDialogService, DefaultFileDialogService>(new ContainerControlledLifetimeManager());
            



            AppCommands();
            LoadSettings();

            
            // Try resolving an output service - if not found, then register the NLog service
            var isDefaultOutputService = false;
            try
            {
                this._outputService = _container.Resolve<IOutputService>();
            }
            catch
            {
                _container.RegisterType<IOutputService, DefaultLogService>(new ContainerControlledLifetimeManager());
                this._outputService = _container.Resolve<IOutputService>();
                isDefaultOutputService = true;
            }


            var app = _container.Resolve<MjolnirApp>();

            if (app != null)
                app.RegisterTypes();

            
            //Output
            OutputModule outputModule = _container.Resolve<OutputModule>();
            outputModule.Initialize();
            
            if (isDefaultOutputService)
                _outputService.LogOutput(new LogOutputItem("DefaultLogService applied", OutputCategory.Info, OutputPriority.None));




            if (app != null)
                app.InitalizeIDE();

            //Below ones can be loaded with solution, does not require immediate load
            //TODO : Console

            PropertiesModule propertiesModule = _container.Resolve<PropertiesModule>();
            propertiesModule.Initialize();


            if (app != null)
            {
                _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Application UI..." });

                app.LoadCommands();
                app.LoadModules();
                app.LoadTheme();
                app.LoadMenus();
                app.LoadToolbar();
                app.LoadSettings();

                
            }

            var shell = _container.Resolve<IShellView>();
            shell.LoadLayout();

            _eventAggregator.GetEvent<IDELoadedEvent>().Publish();
        }

        private void AppCommands()
        {
            var manager = _container.Resolve<ICommandManager>();
            var registry = _container.Resolve<IContentHandlerRegistry>();

            //TODO: Check if you can hook up to the Workspace.ActiveDocument.CloseCommand
            var closeCommand = new DelegateCommand<object>(CloseDocument, CanExecuteCloseDocument);
            manager.RegisterCommand(CommandManagerConstants.CloseCommand, closeCommand);
            manager.RegisterCommand(CommandManagerConstants.NewItem, registry.NewCommand);
        }

        private void LoadSettings()
        {
            //Resolve to get the last used theme from the settings
            //TODO : Update theme
            _container.Resolve<ThemeSettings>();
            
        }


        /// <summary>
        /// Can the close command execute? Checks if there is an ActiveDocument - if present, returns true.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns><c>true</c> if this instance can execute close document; otherwise, <c>false</c>.</returns>
        private bool CanExecuteCloseDocument(object obj)
        {
            ContentViewModel vm = obj as ContentViewModel;
            if (vm != null)
                return true;

            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            return workspace.ActiveDocument != null;
        }

        /// <summary>
        /// CloseDocument method that gets called when the Close command gets executed.
        /// </summary>
        private void CloseDocument(object obj)
        {
            IWorkspace workspace = _container.Resolve<DefaultWorkspace>();
            IOutputService output = _container.Resolve<IOutputService>();
            IMenuService menuService = _container.Resolve<IMenuService>();

            CancelEventArgs e = obj as CancelEventArgs;
            ContentViewModel activeDocument = obj as ContentViewModel;

            if (activeDocument == null)
                activeDocument = workspace.ActiveDocument;

            if (activeDocument.Handler != null && activeDocument.Model.IsDirty)
            {
                //means the document is dirty - show a message box and then handle based on the user's selection
                var res = MessageBox.Show(string.Format("Save changes for document '{0}'?", activeDocument.Title),
                                          "Are you sure?", MessageBoxButton.YesNoCancel);

                //Pressed Yes
                if (res == MessageBoxResult.Yes)
                {
                    if (!workspace.ActiveDocument.Handler.SaveContent(workspace.ActiveDocument))
                    {
                        //Failed to save - return cancel
                        res = MessageBoxResult.Cancel;

                        //Cancel was pressed - so, we cant close
                        if (e != null)
                        {
                            e.Cancel = true;
                        }
                        return;
                    }
                }

                //Pressed Cancel
                if (res == MessageBoxResult.Cancel)
                {
                    //Cancel was pressed - so, we cant close
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    return;
                }
            }

            if (e == null)
            {
                output.LogOutput(new LogOutputItem("Closing document " + activeDocument.Model.Location, OutputCategory.Info, OutputPriority.None));
                workspace.Documents.Remove(activeDocument);
                _eventAggregator.GetEvent<ClosedContentEvent>().Publish(activeDocument);
                menuService.Refresh();
            }
            else
            {
                // If the location is not there - then we can remove it.
                // This can happen when on clicking "No" in the popup and we still want to quit
                if (activeDocument.Model.Location == null)
                {
                    workspace.Documents.Remove(activeDocument);
                    _eventAggregator.GetEvent<ClosedContentEvent>().Publish(activeDocument);
                }
            }
        }
    }
}
