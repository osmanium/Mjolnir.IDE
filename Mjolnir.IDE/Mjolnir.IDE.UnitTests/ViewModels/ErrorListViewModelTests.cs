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

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    public class ErrorListViewModelTests : ViewModelTestBase
    {
        private ErrorViewModel _viewModel;
        private Mock<IErrorToolboxToolbarService> _errorToolboxToolbarService;


        public ErrorListViewModelTests()
        {
            _errorToolboxToolbarService = new Mock<IErrorToolboxToolbarService>();

            var errorListUpdatedEvent = new Mock<ErrorListUpdated>();

            _eventAggregator.Setup(ea => ea.GetEvent<ErrorListUpdated>())
                .Returns(errorListUpdatedEvent.Object);

            _viewModel = new ErrorViewModel(_workspace.Object, 
                                            _errorToolboxToolbarService.Object, 
                                            _eventAggregator.Object);
        }

        [WpfFact]
        public void Should_log_new_error()
        {
            var logid = LogError();

            Assert.Equal(1, _viewModel.ErrorItemCount);
            Assert.Equal(0, _viewModel.WarningItemCount);
            Assert.Equal(0, _viewModel.MessageItemCount);

            Assert.NotEqual(Guid.Empty, logid);
        }

        [WpfFact]
        public void Should_log_new_warning()
        {
            var logid = LogWarning();

            Assert.Equal(0, _viewModel.ErrorItemCount);
            Assert.Equal(1, _viewModel.WarningItemCount);
            Assert.Equal(0, _viewModel.MessageItemCount);

            Assert.NotEqual(Guid.Empty, logid);
        }

        [WpfFact]
        public void Should_log_new_message()
        {
            var logid = LogMessage();

            Assert.Equal(0, _viewModel.ErrorItemCount);
            Assert.Equal(0, _viewModel.WarningItemCount);
            Assert.Equal(1, _viewModel.MessageItemCount);

            Assert.NotEqual(Guid.Empty, logid);
        }


        [WpfFact]
        public void Should_remove_error()
        {
            var logid = LogError();

            Assert.Equal(1, _viewModel.ErrorItemCount);
            Assert.NotEqual(Guid.Empty, logid);

            _viewModel.RemoveError(logid);

            Assert.Equal(0, _viewModel.ErrorItemCount);
            Assert.Equal(0, _viewModel.WarningItemCount);
            Assert.Equal(0, _viewModel.MessageItemCount);
        }

        [WpfFact]
        public void Should_remove_warning()
        {
            var logid = LogWarning();

            Assert.Equal(1, _viewModel.WarningItemCount);
            Assert.NotEqual(Guid.Empty, logid);

            _viewModel.RemoveError(logid);

            Assert.Equal(0, _viewModel.ErrorItemCount);
            Assert.Equal(0, _viewModel.WarningItemCount);
            Assert.Equal(0, _viewModel.MessageItemCount);
        }

        [WpfFact]
        public void Should_remove_message()
        {
            var logid = LogMessage();

            Assert.Equal(1, _viewModel.MessageItemCount);
            Assert.NotEqual(Guid.Empty, logid);

            _viewModel.RemoveError(logid);

            Assert.Equal(0, _viewModel.ErrorItemCount);
            Assert.Equal(0, _viewModel.WarningItemCount);
            Assert.Equal(0, _viewModel.MessageItemCount);
        }


        //TODO : Log multiple errors

        //TODO : Remove single from multiple errors

        //TODO : Show only errors

        //TODO : Show only warnings

        //TODO : Show only messages

        //TODO : Show all

        //TODO : Show error - warnings

        //TODO : Show error - messages

        //TODO : Show warning - messages


        #region Private Methods
        private Guid LogError()
        {
            var log = new ErrorListItem(
                itemType: Sdk.Enums.ErrorListItemType.Error,
                number: 1,
                description: "New error log",
                path: "Error Path",
                line: 1,
                column: 1);

            _viewModel.LogError(log);

            return log.Id;
        }

        private Guid LogWarning()
        {
            var log = new ErrorListItem(
               itemType: Sdk.Enums.ErrorListItemType.Warning,
               number: 1,
               description: "New warning log",
               path: "Warning Path",
               line: 2,
               column: 1);

            _viewModel.LogError(log);

            return log.Id;
        }

        private Guid LogMessage()
        {
            var log = new ErrorListItem(
              itemType: Sdk.Enums.ErrorListItemType.Message,
              number: 1,
              description: "New message log",
              path: "Message Path",
              line: 3,
              column: 1);

            _viewModel.LogError(log);

            return log.Id;
        }
        #endregion

    }
}
