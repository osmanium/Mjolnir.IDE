using Mjolnir.IDE.Core.Modules.Shell.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ShellViewModelTests : ViewModelTestBase
    {
        private ShellViewModel _shellViewModel;

        public ShellViewModelTests()
        {
            //_shellViewModel = new ShellViewModel();
        }

        [WpfFact]
        public void Should_open_shell()
        {

        }


    }
}
