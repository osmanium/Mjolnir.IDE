using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Modules.Error.Interfaces;
using Mjolnir.IDE.Modules.Error.Services;
using Mjolnir.IDE.Modules.Error.ViewModels;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Modules.Error
{
    public class ErrorModule : IModule
    {
        private readonly IUnityContainer _container;

        public ErrorModule(IUnityContainer container)
        {
            _container = container;
        }

        private IEventAggregator EventAggregator
        {
            get { return _container.Resolve<IEventAggregator>(); }
        }

        public void Initialize()
        {
            EventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Error Module..." });
            _container.RegisterType<ErrorViewModel>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            var commandManager = _container.Resolve<ICommandManager>();

            _container.RegisterType<IErrorToolboxToolbarService, ErrorToolbarToolboxService>(new ContainerControlledLifetimeManager());
            
            ErrorViewModel errorViewModel = _container.Resolve<ErrorViewModel>();

            workspace.Tools.Add(errorViewModel);

            var errorToolbarService = _container.Resolve<IErrorToolboxToolbarService>();


            errorToolbarService.Add(new ToolbarViewModel("Standard", 1) { Band = 1, BandIndex = 1 });

            errorToolbarService.Get("Standard")
                               .Add(new MenuItemViewModel("_New", 3,
                                        new BitmapImage(
                                            new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/NewRequest_8796.png")),
                                       commandManager.GetCommand("NEW"),
                                       new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N")));
            

        }
    }
}
