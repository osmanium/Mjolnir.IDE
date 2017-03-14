using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Toolbox.ViewModels;
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

namespace Mjolnir.IDE.Core.Modules.Toolbox
{
    public class ToolboxModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWorkspace _workspace;
        private readonly ICommandManager _commandManager;
        private ToolboxViewModel _toolboxViewModel;

        private IToolboxToolbarService _toolbarService;

        public ToolboxModule(IUnityContainer container, 
                             IEventAggregator eventAggregaor,
                             AbstractWorkspace abstractWorkspace,
                             ICommandManager commandManager)
        {
            _container = container;
            _eventAggregator = eventAggregaor;
            _workspace = abstractWorkspace;
            _commandManager = commandManager;
        }


        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Toolbox..." });

            _container.RegisterType<IToolboxService,ToolboxViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ToolboxViewModel>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IToolboxToolbarService, ToolboxToolbarService>(new ContainerControlledLifetimeManager());

            _toolboxViewModel = _container.Resolve<ToolboxViewModel>();

            _toolbarService = _container.Resolve<IToolboxToolbarService>();



            LoadCommands();
            LoadToolbar();

            _workspace.Tools.Add(_toolboxViewModel);

        }

        private void LoadToolbar()
        {
        }

        private void LoadCommands()
        {
        }
    }
}
