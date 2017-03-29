using Mjolnir.IDE.Core.Modules.SplashScreen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    public class DefaultSplashScreenViewModelTests : ViewModelTestBase
    {
        private DefaultSplashScreenViewModel _viewModel;
        public DefaultSplashScreenViewModelTests()
        {
            _viewModel = new DefaultSplashScreenViewModel(_eventAggregator.Object, _applicationDefinition.Object);
        }

        [WpfFact]
        public void Should_update_message()
        {

        }

    }
}
