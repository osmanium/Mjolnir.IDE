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
        Mock<IErrorToolboxToolbarService> _errorToolboxToolbarService;
        Mock<ErrorListUpdated> _errorListUpdatedEvent;
        ErrorViewModel _errorViewModel;

        public ErrorListViewModelTests()
        {
            _errorToolboxToolbarService = new Mock<IErrorToolboxToolbarService>();
            _errorListUpdatedEvent = new Mock<ErrorListUpdated>();

            _eventAggregator.Setup(ea => ea.GetEvent<ErrorListUpdated>())
                .Returns(_errorListUpdatedEvent.Object);
            _errorViewModel = new ErrorViewModel(_workspace.Object,
                                            _errorToolboxToolbarService.Object,
                                            _eventAggregator.Object);
        }

        [WpfFact]
        public void Should_Log_New_Error()
        {
            var logid = LogError(_errorViewModel);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(1),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(1),
                () => _errorViewModel.WarningItemCount.ShouldBe(0),
                () => _errorViewModel.MessageItemCount.ShouldBe(0)
            );


            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_Log_New_Warning()
        {
            var logid = LogWarning(_errorViewModel);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(1),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(0),
                () => _errorViewModel.WarningItemCount.ShouldBe(1),
                () => _errorViewModel.MessageItemCount.ShouldBe(0)
            );

            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_Log_New_Message()
        {
            var logid = LogMessage(_errorViewModel);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(1),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(0),
                () => _errorViewModel.WarningItemCount.ShouldBe(0),
                () => _errorViewModel.MessageItemCount.ShouldBe(1)
            );

            logid.ShouldNotBe(Guid.Empty);
        }


        [WpfFact]
        public void Should_Remove_Error()
        {
            var logid = LogError(_errorViewModel);

            _errorViewModel.ErrorItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            _errorViewModel.RemoveError(logid);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.ErrorItemCount.ShouldBe(0),
                () => _errorViewModel.WarningItemCount.ShouldBe(0),
                () => _errorViewModel.MessageItemCount.ShouldBe(0)
            );
        }

        [WpfFact]
        public void Should_Remove_Warning()
        {
            var logid = LogWarning(_errorViewModel);

            _errorViewModel.WarningItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            _errorViewModel.RemoveError(logid);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.ErrorItemCount.ShouldBe(0),
                () => _errorViewModel.WarningItemCount.ShouldBe(0),
                () => _errorViewModel.MessageItemCount.ShouldBe(0)
            );
        }

        [WpfFact]
        public void Should_Remove_Message()
        {
            var logid = LogMessage(_errorViewModel);

            _errorViewModel.MessageItemCount.ShouldBe(1);
            logid.ShouldNotBe(Guid.Empty);

            _errorViewModel.RemoveError(logid);

            _errorViewModel.ShouldSatisfyAllConditions(
               () => _errorViewModel.ErrorItemCount.ShouldBe(0),
               () => _errorViewModel.WarningItemCount.ShouldBe(0),
               () => _errorViewModel.MessageItemCount.ShouldBe(0)
           );
        }


        [WpfFact]
        public void Should_Log_Multiple_Types()
        {
            LogItems(_errorViewModel);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(3),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(1),
                () => _errorViewModel.WarningItemCount.ShouldBe(1),
                () => _errorViewModel.MessageItemCount.ShouldBe(1)
            );
        }

        [WpfFact]
        public void Should_Log_Multiple_Types_Repeated()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Error_From_Multiple_Types()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            var log = _errorViewModel.Items.First(f => f.ItemType == ErrorListItemType.Error);
            _errorViewModel.RemoveError(log.Id);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(8),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(2),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Warning_From_Multiple_Types()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            var log = _errorViewModel.Items.First(f => f.ItemType == ErrorListItemType.Warning);
            _errorViewModel.RemoveError(log.Id);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(8),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(2),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Remove_Single_Message_From_Multiple_Types()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            var log = _errorViewModel.Items.First(f => f.ItemType == ErrorListItemType.Message);
            _errorViewModel.RemoveError(log.Id);

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(8),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(0),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(2)
            );
        }



        [WpfFact]
        public void Should_Show_Filtered_Errors()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowErrors = true;

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(3),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Warnings()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowWarnings = true;

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(3),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Messages()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowMessages = true;

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(3),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Errors_And_Warnings()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowErrors = true;
            _errorViewModel.ShowWarnings = true;


            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(6),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Errors_And_Messages()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowErrors = true;
            _errorViewModel.ShowMessages = true;


            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(6),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_Warnings_And_Messages()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

           _errorViewModel.ShowWarnings = true;
            _errorViewModel.ShowMessages = true;


            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(6),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }

        [WpfFact]
        public void Should_Show_Filtered_All_Types()
        {
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);
            LogItems(_errorViewModel);

            _errorViewModel.ShowErrors = true;
            _errorViewModel.ShowWarnings = true;
            _errorViewModel.ShowMessages = true;

            _errorViewModel.ShouldSatisfyAllConditions(
                () => _errorViewModel.Items.Count.ShouldBe(9),
                () => _errorViewModel.FilteredItems.Count().ShouldBe(9),
                () => _errorViewModel.ErrorItemCount.ShouldBe(3),
                () => _errorViewModel.WarningItemCount.ShouldBe(3),
                () => _errorViewModel.MessageItemCount.ShouldBe(3)
            );
        }



        #region Private Methods


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
