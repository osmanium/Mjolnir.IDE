using Mjolnir.IDE.Core.Modules.SplashScreen.ViewModels;
using Mjolnir.IDE.Sdk.Events;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class DefaultSplashScreenViewModelTests : ViewModelTestBase
    {
        private DefaultSplashScreenViewModel _viewModel;
        public DefaultSplashScreenViewModelTests()
        {
            var splashScreenUpdateEvent = new Mock<SplashScreenUpdateEvent>();

            _eventAggregator.Setup(ea => ea.GetEvent<SplashScreenUpdateEvent>())
                .Returns(splashScreenUpdateEvent.Object);

            _viewModel = new DefaultSplashScreenViewModel(_eventAggregator.Object, _mjolnirApp.Object);
        }

        [WpfFact(Skip ="Check the message")]
        public void Should_update_message()
        {
            



        }

    }
}
