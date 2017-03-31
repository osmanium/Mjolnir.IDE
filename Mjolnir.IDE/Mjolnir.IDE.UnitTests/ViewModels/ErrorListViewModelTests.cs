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

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    public class ErrorListViewModelTests : ViewModelTestBase
    {

        [WpfFact]
        public void Should_log_new_error()
        {
            var viewModel = CreateErrorViewModelFactory ();
            var logid = LogError(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.ErrorItemCount.ShouldBe(1),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );


            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_log_new_warning()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogWarning(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(1),
                () => viewModel.MessageItemCount.ShouldBe(0)
            );

            logid.ShouldNotBe(Guid.Empty);
        }

        [WpfFact]
        public void Should_log_new_message()
        {
            var viewModel = CreateErrorViewModelFactory();
            var logid = LogMessage(viewModel);

            viewModel.ShouldSatisfyAllConditions(
                () => viewModel.ErrorItemCount.ShouldBe(0),
                () => viewModel.WarningItemCount.ShouldBe(0),
                () => viewModel.MessageItemCount.ShouldBe(1)
            );

            logid.ShouldNotBe(Guid.Empty);
        }


        [WpfFact]
        public void Should_remove_error()
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
        public void Should_remove_warning()
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
        public void Should_remove_message()
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


        //TODO : Use theory for multiple inserts

        //TODO : Log multiple errors

        //TODO : Remove single from multiple errors

        //TODO : Show only errors

        //TODO : Show only warnings

        //TODO : Show only messages

        //TODO : Show all

        //TODO : Show error - warnings

        //TODO : Show error - messages

        //TODO : Show warning - messages

        //TODO : Check FilteredItems count for all the combinations


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
                description: "New error log",
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
               description: "New warning log",
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
              description: "New message log",
              path: "Message Path",
              line: 3,
              column: 1);

            viewModel.LogError(log);

            return log.Id;
        }
        #endregion

    }
}
