using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests.TestDataSources
{
    public static class OutputTestDataProvider
    {
        private static List<object[]> _testData = new List<object[]>()
        {
            new object[] {new LogOutputItem("message",OutputCategory.Info, OutputPriority.High) }
        };

        public static IEnumerable<object[]> TestData
        {
            get
            {
                return _testData;
            }
        }
    }
}
