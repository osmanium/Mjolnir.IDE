using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Enums;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Core.Modules.ErrorList.Events;
using Mjolnir.IDE.Modules.Error.Interfaces;
using Mjolnir.IDE.Core.Modules.ErrorList.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Mjolnir.IDE.Core.Services;

namespace Mjolnir.IDE.Core.Modules.ErrorList
{
    public class ErrorListModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWorkspace _workspace;
        private readonly ICommandManager _commandManager;
        private ErrorViewModel _errorViewModel;

        private IErrorToolboxToolbarService _errorToolbarService;

        public ErrorListModule(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _workspace = _container.Resolve<AbstractWorkspace>();
            _commandManager = _container.Resolve<ICommandManager>();
        }


        public void Initialize()
        {
            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Error Module..." });

            _container.RegisterType<ErrorViewModel>();
            _container.RegisterType<IErrorToolboxToolbarService, ErrorToolbarToolboxService>(new ContainerControlledLifetimeManager());

            _errorViewModel = _container.Resolve<ErrorViewModel>();

            _workspace.Tools.Add(_errorViewModel);

            _errorToolbarService = _container.Resolve<IErrorToolboxToolbarService>();



            LoadCommands();
            LoadToolbar();

            _eventAggregator.GetEvent<ErrorListUpdated>().Subscribe(UpdateToggleBarTexts);


        }

        private void LoadCommands()
        {
            var manager = _container.Resolve<ICommandManager>();

            var toggleErrorCommand = new DelegateCommand(ToggleErrorList);
            var toggleWarningCommand = new DelegateCommand(ToggleWarningList);
            var toggleInformationCommand = new DelegateCommand(ToggleInformationList);


            manager.RegisterCommand("TOGGLEERRORLIST", toggleErrorCommand);
            manager.RegisterCommand("TOGGLEEWARNINGLIST", toggleWarningCommand);
            manager.RegisterCommand("TOGGLEINFORMATIONLIST", toggleInformationCommand);
        }

        public void ToggleErrorList()
        {
            _errorViewModel.ShowErrors = !_errorViewModel.ShowErrors;
        }

        private void ToggleWarningList()
        {
            _errorViewModel.ShowWarnings = !_errorViewModel.ShowWarnings;
        }

        private void ToggleInformationList()
        {
            _errorViewModel.ShowMessages = !_errorViewModel.ShowMessages;
        }

        

        private void LoadToolbar()
        {
            _errorToolbarService.Add(new ToolbarViewModel("Standard", "Standard", 1) { Band = 1, BandIndex = 1 });

            var menu = _errorToolbarService.Get("Standard");

            var errorToggleButton = new MenuItemViewModel(
                             "_Error", ErrorCountText(), 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Error.png")),
                             _commandManager.GetCommand("TOGGLEERRORLIST"), null, false, false, null, true
                                         );
            menu.Add(errorToggleButton);

            var warningToggleButton = new MenuItemViewModel(
                             "_Warning", WarningCountText(), 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Warning.png")),
                             _commandManager.GetCommand("TOGGLEEWARNINGLIST"), null, false, false, null, true
                                         );
            menu.Add(warningToggleButton);

            var informationToggleButton = new MenuItemViewModel(
                             "_Information", InformationCountText(), 3, new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Message.png")),
                             _commandManager.GetCommand("TOGGLEINFORMATIONLIST"), null, false, false, null, true
                                         );
            menu.Add(informationToggleButton);
        }

        private string ErrorCountText()
        {
            return _errorViewModel.ErrorItemCount.ToString() + " Errors";
        }
        private string WarningCountText()
        {
            return _errorViewModel.WarningItemCount.ToString() + " Warnings";
        }

        private string InformationCountText()
        {
            return _errorViewModel.MessageItemCount.ToString() + " Information";
        }

        private void UpdateToggleBarTexts(ErrorListUpdated e)
        {
            (_errorToolbarService.Get("Standard").Get("_Error") as MenuItemViewModel).Text = ErrorCountText();
            (_errorToolbarService.Get("Standard").Get("_Warning") as MenuItemViewModel).Text = WarningCountText();
            (_errorToolbarService.Get("Standard").Get("_Information") as MenuItemViewModel).Text = InformationCountText();
        }
    }
}
