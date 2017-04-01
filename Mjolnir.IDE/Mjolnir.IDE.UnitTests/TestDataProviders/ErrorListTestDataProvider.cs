using Mjolnir.IDE.Core.Modules.Output.ViewModels;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests.TestDataSources
{
    public static class ErrorListTestDataProvider
    {
        //Default Output Source - Empty Message
        private static List<object[]> _testData_DefaultOutputSource_Empty_Message = new List<object[]>()
        {
            //Null message combinations
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.High) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Low) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Medium) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.None) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.High) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.Low) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.Medium) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.None) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.High) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.Low) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.Medium) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.None) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.High) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Low) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Medium) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.None) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.High) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.Low) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.Medium) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.None) }
        };

        public static IEnumerable<object[]> TestData_DefaultOutputSource_Empty_Message
        {
            get
            {
                return _testData_DefaultOutputSource_Empty_Message;
            }
        }
        
    }
}
