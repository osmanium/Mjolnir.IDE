using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Properties.ViewModels;
using Mjolnir.IDE.Core.Services;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Properties
{
    public class PropertiesModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWorkspace _workspace;
        private readonly ICommandManager _commandManager;
        private PropertiesViewModel _propertiesViewModel;

        private IPropertiesToolboxToolbarService _propertiesToolbarService;


        public PropertiesModule(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _workspace = _container.Resolve<AbstractWorkspace>();
            _commandManager = _container.Resolve<ICommandManager>();
        }

        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Properties..." });

            _container.RegisterType<PropertiesViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPropertiesToolboxToolbarService, PropertiesToolboxToolbarService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IPropertyGrid, PropertiesViewModel>(new ContainerControlledLifetimeManager());

            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            _propertiesViewModel = _container.Resolve<PropertiesViewModel>();
            _propertiesToolbarService = _container.Resolve<IPropertiesToolboxToolbarService>();

            LoadCommands();
            LoadToolbar();

            workspace.Tools.Add(_propertiesViewModel);
        }

        private void LoadCommands()
        {
            
        }

        private void LoadToolbar()
        {
            
        }
    }
}
