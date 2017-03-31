using Mjolnir.IDE.Core.Modules.Output.ViewModels;
using Mjolnir.IDE.Core.UnitTests.TestDataSources;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;
using Shouldly;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    public class OutputViewModelTests : ViewModelTestBase
    {
        private Mock<IOutputToolboxToolbarService> _outputToolboxToolbarService;
        private IEnumerable<object> _logDataSource = new List<object>();


        public OutputViewModelTests()
        {
            var logEvent = new Mock<LogOutputEvent>();
            var outputSourceChangedEvent = new Mock<OutputSourceChangedEvent>();
            var outputSourceAddedEvent = new Mock<OutputSourceAddedEvent>();
            var outputSourceRemovedEvent = new Mock<OutputSourceRemovedEvent>();

            _eventAggregator.Setup(ea => ea.GetEvent<LogOutputEvent>())
                .Returns(logEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceChangedEvent>())
                .Returns(outputSourceChangedEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceAddedEvent>())
                .Returns(outputSourceAddedEvent.Object);
            _eventAggregator.Setup(ea => ea.GetEvent<OutputSourceRemovedEvent>())
                .Returns(outputSourceRemovedEvent.Object);

            _outputToolboxToolbarService = new Mock<IOutputToolboxToolbarService>();



        }

        [WpfFact]
        public void Should_log_new_output()
        {
            var viewModel = ViewModelFactory();


        }


        [WpfTheory]
        [MemberData("TestData", MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_New_Log(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();
            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(log);

            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
        }

        private OutputViewModel ViewModelFactory()
        {
            return new OutputViewModel(_workspace.Object,
                                                         _outputToolboxToolbarService.Object,
                                                         _commandManager.Object,
                                                         _eventAggregator.Object,
                                                         _outputService.Object);
        }
    }
}
