﻿using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Services;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Modules.Error;
using Mjolnir.IDE.Modules.Output;
using Mjolnir.IDE.Modules.Settings;
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

        public CoreModule(IUnityContainer container,
                          IEventAggregator eventAggregator)
        {
            this._container = container;
            this._eventAggregator = eventAggregator;
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


            

            
            AppCommands();
            LoadSettings();


           


            

            

            //Try resolving a workspace
            var isDefaultWorkspace = false;
            try
            {
                _container.Resolve<AbstractWorkspace>();
            }
            catch
            {
                _container.RegisterType<AbstractWorkspace, DefaultWorkspace>(new ContainerControlledLifetimeManager());
                isDefaultWorkspace = true;
            }

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

            //Output
            OutputModule outputModule = _container.Resolve<OutputModule>();
            outputModule.Initialize();
            
            if (isDefaultOutputService)
                _outputService.LogOutput("DefaultLogService applied", OutputCategory.Info, OutputPriority.None);

            if (isDefaultWorkspace)
                _outputService.LogOutput("DefaultWorkspace applied", OutputCategory.Info, OutputPriority.None);


            ErrorModule errorModule = _container.Resolve<ErrorModule>();
            errorModule.Initialize();




            var customApplication = _container.Resolve<IApplicationDefinition>();
            if (customApplication != null)
            {
                customApplication.InitalizeIDE();
                customApplication.RegisterTypes();
            }

            

            //Below ones can be loaded with solution, does not require immediate load
            //TODO : Console
            //TODO : Error


            if (customApplication != null)
            {
                _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Application UI..." });

                customApplication.LoadCommands();
                customApplication.LoadTheme();
                customApplication.LoadMenus();
                customApplication.LoadToolbar();
                customApplication.LoadSettings();

                customApplication.LoadModules();
            }

            var shell = _container.Resolve<IShellView>();
            shell.LoadLayout();

            var applicationDefinition = _container.Resolve<IApplicationDefinition>();
            if (applicationDefinition != null)
                applicationDefinition.OnIDELoaded();
        }

        private void AppCommands()
        {
            var manager = _container.Resolve<ICommandManager>();
            var registry = _container.Resolve<IContentHandlerRegistry>();

            //TODO: Check if you can hook up to the Workspace.ActiveDocument.CloseCommand
            var closeCommand = new DelegateCommand<object>(CloseDocument, CanExecuteCloseDocument);
            manager.RegisterCommand("CLOSE", closeCommand);
            manager.RegisterCommand("NEW", registry.NewCommand);
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

            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            return workspace.ActiveDocument != null;
        }

        /// <summary>
        /// CloseDocument method that gets called when the Close command gets executed.
        /// </summary>
        private void CloseDocument(object obj)
        {
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
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
                output.LogOutput("Closing document " + activeDocument.Model.Location, OutputCategory.Info, OutputPriority.None);
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
