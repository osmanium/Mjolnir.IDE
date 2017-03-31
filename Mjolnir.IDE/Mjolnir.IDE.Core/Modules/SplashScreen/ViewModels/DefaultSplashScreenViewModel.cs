using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.ViewModels;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Mjolnir.IDE.Core.Modules.SplashScreen.ViewModels
{
    public class DefaultSplashScreenViewModel : ValidatableBindableBase, ISplashScreenViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        //private readonly IUnityContainer _container;
        private readonly MjolnirApp _app;

        private string _status;
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        private string _applicationName;
        public string ApplicationName
        {
            get { return _applicationName; }
            set { SetProperty(ref _applicationName, value); }
        }

        private ImageSource _applicationIconSource;
        public ImageSource ApplicationIconSource
        {
            get { return _applicationIconSource; }
            set { _applicationIconSource = value; _applicationIconSource.Freeze(); }
        }

        public DefaultSplashScreenViewModel(IEventAggregator eventAggregator,
                                            MjolnirApp app)
        {
            _app = app;
            _eventAggregator = eventAggregator;

            if (!string.IsNullOrWhiteSpace(app.ApplicationName))
            {
                ApplicationName = app.ApplicationName;
            }
            else
            {
                ApplicationName = "Mjolnir.IDE";
                app.ApplicationName = ApplicationName;
            }


            if (app?.ApplicationIconSource != null)
            {
                ApplicationIconSource = app.ApplicationIconSource;
            }
            else
            {
                ApplicationIconSource = new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Mjolnir_Icon.png"));
                app.ApplicationIconSource = ApplicationIconSource;
            }

            _eventAggregator.GetEvent<SplashScreenUpdateEvent>().Subscribe(e => UpdateMessage(e.Text));
            IsValidationEnabled = false;
        }

        private void UpdateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Status = message;
        }
    }
}
