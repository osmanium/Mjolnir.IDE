using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestStack.White;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using Xunit;

namespace Mjolnir.IDE.Test.UITest
{
    public class SmokeTest
    {
        [Fact(Skip ="Check why window title property is not set")]
        public void should_open_ide()
        {
            Application application = Application.Launch(@"C:\Prj\GitHub\Mjolnir.IDE\Mjolnir.IDE\Mjolnir.IDE.Test\bin\Debug\Mjolnir.IDE.Test.exe");
            Window window = application.GetWindow("Mjolnir.IDE");

            var menuBar = window.MenuBar;
            TestStack.White.UIItems.MenuItems.Menu selectFileOpen = menuBar.MenuItem("File", "New");
            selectFileOpen.Click();

            application.Close();
        }
    }
}
