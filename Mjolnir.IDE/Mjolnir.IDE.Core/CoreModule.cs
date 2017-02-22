using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Services;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Modules.Settings;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Core
{
    public class CoreModule : IModule
    {
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;

        public CoreModule(IUnityContainer container,
                          IEventAggregator eventAggregator)
        {
            this._container = container;
            this._eventAggregator = eventAggregator;
        }

        public Action ApplicationDefinition { get; set; }

        public void Initialize()
        {

            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Components..." });
            Thread.Sleep(1000);



            //_container.RegisterType<TextViewModel>();
            //_container.RegisterType<TextModel>();
            //_container.RegisterType<TextView>();
            //_container.RegisterType<AllFileHandler>();
            _container.RegisterType<IThemeSettings, ThemeSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRecentViewSettings, RecentViewSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWindowPositionSettings, WindowPositionSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IToolbarPositionSettings, ToolbarPositionSettings>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ICommandManager, Mjolnir.IDE.Core.Services.CommandManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IContentHandlerRegistry, ContentHandlerRegistry>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IStatusbarService, MjolnirStatusbarViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IThemeManager, ThemeManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IToolbarService, ToolbarService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMenuService, MenuItemViewModel>(new ContainerControlledLifetimeManager(),
                                                                     new InjectionConstructor(
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
                                                                             typeof(IUnityContainer), _container)));
            _container.RegisterType<ToolbarViewModel>(
                new InjectionConstructor(new InjectionParameter(typeof(string), "$MAIN$"),
                                         new InjectionParameter(typeof(int), 1),
                                         new InjectionParameter(typeof(ImageSource), null),
                                         new InjectionParameter(typeof(ICommand), null),
                                         new InjectionParameter(typeof(bool), false),
                                         new InjectionParameter(typeof(IUnityContainer), _container)));

            _container.RegisterType<ISettingsManager, SettingsManager>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IOpenDocumentService, OpenDocumentService>(new ContainerControlledLifetimeManager());

            //TODO : Check here
            //AppCommands();
            //LoadSettings();

            //Try resolving a workspace
            try
            {
                _container.Resolve<AbstractWorkspace>();
            }
            catch
            {
                _container.RegisterType<AbstractWorkspace, Workspace>(new ContainerControlledLifetimeManager());
            }

            // Try resolving a logger service - if not found, then register the NLog service
            try
            {
                _container.Resolve<ILoggerService>();
            }
            catch
            {
                _container.RegisterType<ILoggerService, DefaultLogService>(new ContainerControlledLifetimeManager());
            }

            //Register a default file opener
            var registry = _container.Resolve<IContentHandlerRegistry>();
            //registry.Register(_container.Resolve<AllFileHandler>());







            //Maybe load workspace first
            //Application.Current.MainWindow.DataContext = Container.Resolve<AbstractWorkspace>();

            //TODO : Load other modules here


            //TODO : Settings
            //TODO : Toolbar
            //TODO : Statusbar

            //Below ones can be loaded with solution, does not require immediate load
            //TODO : Console
            //TODO : Error


            if (ApplicationDefinition != null)
            {
                _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Application UI..." });
                ApplicationDefinition.Invoke();
            }
        }
    }
}
