using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Events;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public void Initialize()
        {
            

            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Publish(new SplashScreenUpdateEvent { Text = "Loading Components..." });
            Thread.Sleep(1000);

            //Maybe load workspace first
            //Application.Current.MainWindow.DataContext = Container.Resolve<AbstractWorkspace>();

            //TODO : Load other modules here


            //TODO : Settings
            //TODO : Toolbar
            //TODO : Statusbar

            //Below ones can be loaded with solution, does not require immediate load
            //TODO : Console
            //TODO : Error



        }
    }
}
