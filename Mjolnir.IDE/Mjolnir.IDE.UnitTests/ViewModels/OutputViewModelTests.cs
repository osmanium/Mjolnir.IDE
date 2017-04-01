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
using System.Diagnostics.CodeAnalysis;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    [ExcludeFromCodeCoverage]
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

        
        [WpfTheory]
        [MemberData(nameof(OutputTestDataProvider.TestData_DefaultOutputSource_Empty_Message), MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_New_Log_With_Null_Output_Source_And_Null_Message(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();
            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(log);

            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }

        [WpfTheory]
        [MemberData(nameof(OutputTestDataProvider.TestData_DefaultOutputSource_NonEmpty_Message), MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_New_Log_With_Null_Output_Source_And_NonEmpty_Message(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();
            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(log);

            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
        }

        [WpfTheory]
        [MemberData(nameof(OutputTestDataProvider.TestData_SpecificOutputSource_Empty_Message), MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_New_Log_With_Specific_Output_Source_And_Null_Message(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();
            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(log);

            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }

        [WpfTheory]
        [MemberData(nameof(OutputTestDataProvider.TestData_SpecificOutputSource_NonEmpty_Message), MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_New_Log_With_Specific_Output_Source_And_NonEmpty_Message(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();
            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(log);

            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
        }



        [WpfTheory]
        [MemberData(nameof(OutputTestDataProvider.TestData_DefaultOutputSource_NonEmpty_Message), MemberType = typeof(OutputTestDataProvider))]
        public void Should_Add_Clear_Current_Context_Log_For_Default_OutputSource(LogOutputItem log)
        {
            var viewModel = ViewModelFactory();

            viewModel.Text.ShouldBeNullOrWhiteSpace();
            viewModel.AddLog(log);
            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
            
            viewModel.ClearCurrentContextLog();

            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }



        [WpfFact]
        public void Should_Add_New_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "NewOutputSource";
            
            viewModel.OutputSource.Keys.Count.ShouldBe(1);
            viewModel.AddOutputSource(outputSourceName);
            viewModel.OutputSource.Keys.Count.ShouldBe(2);
        }


        [WpfFact]
        public void Should_Add_New_OutputSource_And_Current_Should_Be_Default()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "NewOutputSource";

            viewModel.CurrentOutputContext.ShouldBe(OutputViewModel.DefaultOutputSource);
            viewModel.AddOutputSource(outputSourceName);
            viewModel.CurrentOutputContext.ShouldBe(OutputViewModel.DefaultOutputSource);

            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }

        [WpfFact]
        public void Should_Add_New_OutputSource_And_Add_New_Log()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "NewOutputSource";

            viewModel.AddOutputSource(outputSourceName);

            viewModel.OutputSource[outputSourceName].ShouldBeNullOrWhiteSpace();

            viewModel.AddLog(new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None, outputSourceName));

            viewModel.Text.ShouldBeNullOrWhiteSpace();
            viewModel.CurrentOutputContext = outputSourceName;
            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
        }

        [WpfFact]
        public void Should_Add_Existing_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "General";

            Should.Throw<ArgumentException>(() =>
            {
                viewModel.AddOutputSource(outputSourceName);
            });
        }


        [WpfFact]
        public void Should_Remove_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "NewOutputSource";

            viewModel.AddOutputSource(outputSourceName);
            viewModel.OutputSource.Count.ShouldBe(2);

            viewModel.RemoveOutputSource(outputSourceName);
            viewModel.OutputSource.Count.ShouldBe(1);
        }

        [WpfFact]
        public void Should_Remove_Nonexisting_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";

            Should.Throw<ArgumentException>(() =>
            {
                viewModel.RemoveOutputSource(outputSourceName);
            });
        }



        [WpfFact]
        public void Should_Output_Source_Changed_Event_Fired()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";
            var isOutputSourceChangedEventCalled = false;

            var outputSourceChangedEvent = new Mock<OutputSourceChangedEvent>(outputSourceName);

            _eventAggregator.Setup(s => s.GetEvent<OutputSourceChangedEvent>())
                .Returns(outputSourceChangedEvent.Object)
                .Callback(() => 
                {
                    isOutputSourceChangedEventCalled = true;
                }) ;

            viewModel.AddOutputSource(outputSourceName);
            viewModel.CurrentOutputContext = outputSourceName;

            isOutputSourceChangedEventCalled.ShouldBe(true);
        }

        [WpfFact]
        public void Should_Output_Source_Changed_Event_Change_Text()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";
            
            viewModel.AddOutputSource(outputSourceName);

            var log = new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None, outputSourceName);
            viewModel.AddLog(log);

            viewModel.Text.ShouldBeNullOrWhiteSpace();

            viewModel.CurrentOutputContext = outputSourceName;

            viewModel.Text.ShouldNotBeNullOrWhiteSpace();
        }


        [WpfFact]
        public void Should_Output_Source_Added_Event_Fired()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";
            var isOutputSourceAddedEventCalled = false;

            var outputSourceAddedEvent = new Mock<OutputSourceAddedEvent>(outputSourceName);

            _eventAggregator.Setup(s => s.GetEvent<OutputSourceAddedEvent>())
                .Returns(outputSourceAddedEvent.Object)
                .Callback(() =>
                {
                    isOutputSourceAddedEventCalled = true;
                });

            viewModel.AddOutputSource(outputSourceName);

            isOutputSourceAddedEventCalled.ShouldBe(true);
        }

        [WpfFact]
        public void Should_Output_Source_Added_Event_Not_Change_Text()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";

            viewModel.Text.ShouldBeNullOrWhiteSpace();
            viewModel.AddOutputSource(outputSourceName);
            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }


        [WpfFact]
        public void Should_Output_Source_Removed_Event_Fired()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";
            var isOutputSourceRemovedEventCalled = false;

            var outputSourceRemovedEvent = new Mock<OutputSourceRemovedEvent>(outputSourceName);

            _eventAggregator.Setup(s => s.GetEvent<OutputSourceRemovedEvent>())
                .Returns(outputSourceRemovedEvent.Object)
                .Callback(() =>
                {
                    isOutputSourceRemovedEventCalled = true;
                });

            viewModel.OutputSource.Keys.Count.ShouldBe(1);
            viewModel.AddOutputSource(outputSourceName);
            viewModel.OutputSource.Keys.Count.ShouldBe(2);

            viewModel.RemoveOutputSource(outputSourceName);
            viewModel.OutputSource.Keys.Count.ShouldBe(1);

            isOutputSourceRemovedEventCalled.ShouldBe(true);
        }

        [WpfFact]
        public void Should_Output_Source_Removed_Event_Not_Change_Text_If_Not_Current_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";
            
            viewModel.AddOutputSource(outputSourceName);

            viewModel.Text.ShouldBeNullOrWhiteSpace();
            var log = new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None, outputSourceName);
            viewModel.AddLog(log);
            viewModel.Text.ShouldBeNullOrWhiteSpace();
        }

        [WpfFact]
        public void Should_Output_Source_Removed_Event_Not_Change_Text_For_Current_OutputSource()
        {
            var viewModel = ViewModelFactory();
            var outputSourceName = "RandomOutputSource";

            viewModel.AddOutputSource(outputSourceName);
            viewModel.CurrentOutputContext = outputSourceName;

            viewModel.Text.ShouldBeNullOrWhiteSpace();
            var log = new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None, outputSourceName);
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
