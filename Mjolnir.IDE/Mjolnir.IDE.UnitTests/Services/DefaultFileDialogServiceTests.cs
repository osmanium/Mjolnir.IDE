using Microsoft.Win32;
using Mjolnir.IDE.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mjolnir.IDE.Core.UnitTests.Services
{
    public class DefaultFileDialogServiceTests
    {
        //Dialog service is not possible to unit test, it can be tested with other services with mocking

        DefaultFileDialogService _fileDialogService;
        Mock<DefaultMessageDialogService> _messageDialogService;

        public DefaultFileDialogServiceTests()
        {
            _messageDialogService = new Mock<DefaultMessageDialogService>();
            _fileDialogService = new DefaultFileDialogService(_messageDialogService.Object);
        }
    }
}
