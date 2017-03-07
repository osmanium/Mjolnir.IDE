using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Prism.Events;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mjolnir.IDE.Core.Modules.SplashScreen
{
    public class SplashScreenModule : ISplashScreenModule
    {
        private readonly IUnityContainer container;
        private readonly IShellView shell;
        private readonly IEventAggregator eventAggregator;

        private AutoResetEvent WaitForCreation { get; set; }


        public SplashScreenModule(IUnityContainer container,
                                  IEventAggregator eventAggregator,
                                  IShellView shell,
                                  ISplashScreenViewModel splashScreenViewModel)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;
            this.shell = shell;
        }

        public void Initialize()
        {
            Dispatcher.CurrentDispatcher.BeginInvoke(
                (Action)(() =>
                {
                    shell.Show();
                    eventAggregator.GetEvent<SplashScreenCloseEvent>().Publish(new SplashScreenCloseEvent());
                })
            );

            WaitForCreation = new AutoResetEvent(false);

            ThreadStart showSplash =
              () =>
              {
                  Dispatcher.CurrentDispatcher.BeginInvoke(
                    (Action)(() =>
                    {
                        var splashView = container.Resolve<ISplashScreenView>();
                        var splashViewModel = container.Resolve<ISplashScreenViewModel>();

                        splashView.DataContext = splashViewModel;

                        eventAggregator.GetEvent<SplashScreenCloseEvent>().Subscribe(e => splashView.Dispatcher.BeginInvoke((Action)splashView.Close), ThreadOption.PublisherThread, true);

                        splashView.Show();

                        WaitForCreation.Set();
                    }));

                  Dispatcher.Run();
              };
            
            var thread = new Thread(showSplash) { Name = "Splash Thread", IsBackground = true };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            WaitForCreation.WaitOne();
        }
    }
}
