using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Core.Modules.Output.ViewModels;
using Mjolnir.IDE.Core.Modules.Output.Views;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Prism.Commands;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using System.Windows.Media.Imaging;
using Mjolnir.IDE.Core.Services;

namespace Mjolnir.IDE.Core.Modules.Output
{
    public class OutputModule : IModule
    {

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWorkspace _workspace;
        private readonly ICommandManager _commandManager;
        private OutputViewModel _outputViewModel;

        private IOutputToolboxToolbarService _outputToolbarService;


        public OutputModule(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _workspace = _container.Resolve<AbstractWorkspace>();
            _commandManager = _container.Resolve<ICommandManager>();
        }
        
        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Output Module" });

            _container.RegisterType<OutputViewModel>();
            _container.RegisterType<IOutputToolboxToolbarService, OutputToolboxToolbarService>(new ContainerControlledLifetimeManager());


            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            
            _outputViewModel = _container.Resolve<OutputViewModel>();
            _outputToolbarService = _container.Resolve<IOutputToolboxToolbarService>();

            LoadCommands();
            LoadToolbar();

            workspace.Tools.Add(_outputViewModel);
        }

        private void LoadCommands()
        {
            var manager = _container.Resolve<ICommandManager>();

            var clearOutputCommand = new DelegateCommand(ClearOutput);
            
            manager.RegisterCommand("CLEAROUTPUT", clearOutputCommand);
        }

        private void LoadToolbar()
        {
            _outputToolbarService.Add(new ToolbarViewModel("Standard", "Standard", 1) { Band = 1, BandIndex = 1 });

            var menu = _outputToolbarService.Get("Standard");

            var errorToggleButton = new MenuItemViewModel(
                             "_Clear All", "Clear All", 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Clearwindowcontent_6304.png")),
                             _commandManager.GetCommand("CLEAROUTPUT"), null, false, false, null, false
                                         );
            menu.Add(errorToggleButton);

        }

        private void ClearOutput()
        {
            _outputViewModel.ClearLog();
        }
    }
}