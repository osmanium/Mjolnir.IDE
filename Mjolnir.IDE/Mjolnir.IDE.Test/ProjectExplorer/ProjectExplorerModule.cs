using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Test.ProjectExplorer.Interfaces;
using Mjolnir.IDE.Test.ProjectExplorer.Services;
using Mjolnir.IDE.Test.ProjectExplorer.ViewModels;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.ProjectExplorer
{
    public class ProjectExplorerModule : IModule
    {

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly DefaultWorkspace _workspace;
        private readonly ICommandManager _commandManager;

        private ProjectExplorerViewModel _viewModel;

        public ProjectExplorerModule(IUnityContainer container,
                             IEventAggregator eventAggregaor,
                             DefaultWorkspace abstractWorkspace,
                             ICommandManager commandManager)
        {
            _container = container;
            _eventAggregator = eventAggregaor;
            _workspace = abstractWorkspace;
            _commandManager = commandManager;
        }


        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Project Explorer..." });


            _container.RegisterType<ProjectExplorerViewModel>();
            _container.RegisterType<IProjectExplorerToolboxToolbarService, ProjectExplorerToolboxToolbarService>(new ContainerControlledLifetimeManager());

            _viewModel = _container.Resolve<ProjectExplorerViewModel>();
            

            LoadCommands();
            LoadToolbar();

            _workspace.Tools.Add(_viewModel);
        }

        private void LoadCommands()
        {
            var manager = _container.Resolve<ICommandManager>();

        }

        private void LoadToolbar()
        {
            
        }
    }
}
