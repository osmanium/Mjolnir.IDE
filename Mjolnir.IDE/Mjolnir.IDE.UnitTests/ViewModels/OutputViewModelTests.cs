using Mjolnir.IDE.Core.Modules.Output.ViewModels;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    public class OutputViewModelTests : ViewModelTestBase
    {
        private OutputViewModel _viewModel;
        private Mock<IOutputToolboxToolbarService> _outputToolboxToolbarService;

        public OutputViewModelTests()
        {

            var logEvent = new Mock<LogEvent>();
            var outputSourceChangedEvent = new Mock<OutputSourceChangedEvent>();
            var outputSourceAddedEvent = new Mock<OutputSourceAddedEvent>();
            var outputSourceRemovedEvent = new Mock<OutputSourceRemovedEvent>();



            _eventAggregator.Setup(ea => ea.GetEvent<LogEvent>())
                .Returns(logEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceChangedEvent>())
                .Returns(outputSourceChangedEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceAddedEvent>())
                .Returns(outputSourceAddedEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceRemovedEvent>())
                .Returns(outputSourceRemovedEvent.Object);


            _outputToolboxToolbarService = new Mock<IOutputToolboxToolbarService>();

            _viewModel = new OutputViewModel(_workspace.Object,
                                             _outputToolboxToolbarService.Object,
                                             _commandManager.Object,
                                             _eventAggregator.Object);
        }

        [WpfFact]
        public void Should_log_new_output()
        {

        }
    }
}
