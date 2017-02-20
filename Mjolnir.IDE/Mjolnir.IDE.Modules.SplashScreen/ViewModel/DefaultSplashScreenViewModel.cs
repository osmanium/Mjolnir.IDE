using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.SplashScreen.ViewModel
{
    public class DefaultSplashScreenViewModel : ValidatableBindableBase, ISplashScreenViewModel
    {
        private IEventAggregator eventAggregator;


        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                SetProperty(ref _status, value);
            }
        }


        public DefaultSplashScreenViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SplashScreenUpdateEvent>().Subscribe(e => UpdateMessage(e.Text));
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
