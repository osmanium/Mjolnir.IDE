using Mjolnir.IDE.Core.Modules.ErrorList.Events;
using Mjolnir.IDE.Core.Modules.ErrorList.ViewModels;
using Mjolnir.IDE.Modules.Error.Interfaces;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using Mjolnir.IDE.Sdk.Enums;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ErrorListViewModelTests : ViewModelTestBase
    {

        [WpfFact]
        public void Should_Log_New_Error()
        {
            var viewModel = CreateErrorViewModelFactory ();
            var logid = LogError(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(1),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(1),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );


            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_Log_New_Warning()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogWarning(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(1),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(1),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );

            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_Log_New_Message()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogMessage(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(1),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(1)
            );

            logid.ShouldNotBe(Guid.Empty);
        }


        [WpfFact]
        public void Should_Remove_Error()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogError(viewModel);

            viewModel.ErrorItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            viewModel.RemoveError(logid);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );
        }

        [WpfFact]
        public void Should_Remove_Warning()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogWarning(viewModel);

            viewModel.WarningItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            viewModel.RemoveError(logid);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );
        }

        [WpfFact]
        public void Should_Remove_Message()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogMessage(viewModel);

            viewModel.MessageItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            viewModel.RemoveError(logid);

            viewModel.ShouldSatisfyAllConditions(
               () => viewModel.ErrorItemCount.ShouldBe(0),
               () => viewModel.WarningItemCount.ShouldBe(0),
               () => viewModel.MessageItemCount.ShouldBe(0)
           );
        }

        
        [WpfFact]
        public void Should_Log_Multiple_Types()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(3),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(1),
                () => viewModel.WarningItemCount.ShouldBe(1),
                () => viewModel.MessageItemCount.ShouldBe(1)
            );
        }

        [WpfFact]
        public void Should_Log_Multiple_Types_Repeated()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Error_From_Multiple_Types()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            var log = viewModel.Items.First(f=>f.ItemType == ErrorListItemType.Error);
            viewModel.RemoveError(log.Id);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(8),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(2),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Warning_From_Multiple_Types()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            var log = viewModel.Items.First(f => f.ItemType == ErrorListItemType.Warning);
            viewModel.RemoveError(log.Id);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(8),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(2),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Message_From_Multiple_Types()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            var log = viewModel.Items.First(f => f.ItemType == ErrorListItemType.Message);
            viewModel.RemoveError(log.Id);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(8),
                () => viewModel.FilteredItems.Count().ShouldBe(0),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(2)
            );
        }


        
        [WpfFact]
        public void Should_Show_Filtered_Errors()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowErrors = true;

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(3),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Warnings()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowWarnings = true;

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(3),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Messages()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowMessages = true;

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(3),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }
        
        [WpfFact]
        public void Should_Show_Filtered_Errors_And_Warnings()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowErrors = true;
            viewModel.ShowWarnings = true;


            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(6),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Errors_And_Messages()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowErrors = true;
            viewModel.ShowMessages = true;


            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(6),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Warnings_And_Messages()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowWarnings = true;
            viewModel.ShowMessages = true;


            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(6),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_All_Types()
        {
            var viewModel = CreateErrorViewModelFactory();

            LogItems(viewModel);
            LogItems(viewModel);
            LogItems(viewModel);

            viewModel.ShowErrors = true;
            viewModel.ShowWarnings = true;
            viewModel.ShowMessages = true;
            
            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.Items.Count.ShouldBe(9),
                () => viewModel.FilteredItems.Count().ShouldBe(9),
                () => viewModel.ErrorItemCount.ShouldBe(3),
                () => viewModel.WarningItemCount.ShouldBe(3),
                () => viewModel.MessageItemCount.ShouldBe(3)
            );
        }

        

        #region Private Methods
        private ErrorViewModel CreateErrorViewModelFactory()
        {
            Mock<IErrorToolboxToolbarService> _errorToolboxToolbarService = new Mock<IErrorToolboxToolbarService>();

            var errorListUpdatedEvent = new Mock<ErrorListUpdated>();

            _eventAggregator.Setup(ea => ea.GetEvent<ErrorListUpdated>())
                .Returns(errorListUpdatedEvent.Object);

            return new ErrorViewModel(_workspace.Object,
                                            _errorToolboxToolbarService.Object,
                                            _eventAggregator.Object);
        }

        private Guid LogError(ErrorViewModel viewModel)
        {
            var log = new ErrorListItem(
                itemType: Sdk.Enums.ErrorListItemType.Error,
                number: 1,
                description: "New error log " + DateTime.Now.Ticks,
                path: "Error Path",
                line: 1,
                column: 1);

            viewModel.LogError(log);

            return log.Id;
        }

        private Guid LogWarning(ErrorViewModel viewModel)
        {
            var log = new ErrorListItem(
               itemType: Sdk.Enums.ErrorListItemType.Warning,
               number: 1,
               description: "New warning log " + DateTime.Now.Ticks,
               path: "Warning Path",
               line: 2,
               column: 1);

            viewModel.LogError(log);

            return log.Id;
        }

        private Guid LogMessage(ErrorViewModel viewModel)
        {
            var log = new ErrorListItem(
              itemType: Sdk.Enums.ErrorListItemType.Message,
              number: 1,
              description: "New message log " + DateTime.Now.Ticks,
              path: "Message Path",
              line: 3,
              column: 1);

            viewModel.LogError(log);

            return log.Id;
        }

        private void LogItems(ErrorViewModel viewModel)
        {
            LogError(viewModel);
            LogWarning(viewModel);
            LogMessage(viewModel);
        }
        #endregion

    }
}
