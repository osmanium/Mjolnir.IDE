using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Modules.Output.ViewModels;
using Mjolnir.IDE.Modules.Output.Views;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Output
{
    public class OutputModule : IModule
    {

        private readonly IUnityContainer _container;

        public OutputModule(IUnityContainer container)
        {
            _container = container;
        }

        private IEventAggregator EventAggregator
        {
            get { return _container.Resolve<IEventAggregator>(); }
        }
        
        public void Initialize()
        {
            EventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Output Module" });
            _container.RegisterType<OutputViewModel>();
            IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

            OutputViewModel outputViewModel = _container.Resolve<OutputViewModel>();

            workspace.Tools.Add(outputViewModel);
        }
    }
}