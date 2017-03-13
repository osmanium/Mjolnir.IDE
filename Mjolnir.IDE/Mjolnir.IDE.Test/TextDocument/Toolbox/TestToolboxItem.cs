using Mjolnir.IDE.Infrastructure.Attributes;
using Mjolnir.IDE.Test.TextDocument.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.TextDocument.Toolbox
{
    [ToolboxItem(typeof(TextViewModel), "Test Toolbox Item", "Test Category", "pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Output_16xLG.png")]
    public class TestToolboxItem
    {
        public TestToolboxItem()
        {

        }
    }
}
