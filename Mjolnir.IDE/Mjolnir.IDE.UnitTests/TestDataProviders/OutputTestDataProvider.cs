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
    public static class OutputTestDataProvider
    {
        static string defaultOutputSource = OutputViewModel.DefaultOutputSource;

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


        //Default Output Source - With Message
        private static List<object[]> _testData_DefaultOutputSource_NonEmpty_Message = new List<object[]>()
        {
            //With any string values for message combinations
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.High) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Low) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Medium) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.None) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.High) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.Low) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.Medium) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.None) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.High) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.Low) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.Medium) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.None) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.High) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Low) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Medium) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.None) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.High) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.Low) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.Medium) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None) }
        };

        public static IEnumerable<object[]> TestData_DefaultOutputSource_NonEmpty_Message
        {
            get
            {
                return _testData_DefaultOutputSource_NonEmpty_Message;
            }
        }


        //Specific Output Source - Empty Message
        private static List<object[]> _testData_SpecificOutputSource_Empty_Message = new List<object[]>()
        {
            //Null message combinations
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Error, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Exception, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Info, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem(string.Empty, OutputCategory.Warn, OutputPriority.None, defaultOutputSource) },


            };

        public static IEnumerable<object[]> TestData_SpecificOutputSource_Empty_Message
        {
            get
            {
                return _testData_SpecificOutputSource_Empty_Message;
            }
        }


        //Specific Output Source - With Message
        private static List<object[]> _testData_SpecificOutputSource_NonEmpty_Message = new List<object[]>()
        {

            //With any string values for message combinations
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Error, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Exception, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Info, OutputPriority.None, defaultOutputSource) },

            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.High, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.Low, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.Medium, defaultOutputSource) },
            new object[] {new LogOutputItem("Test message", OutputCategory.Warn, OutputPriority.None, defaultOutputSource) }
           };

        public static IEnumerable<object[]> TestData_SpecificOutputSource_NonEmpty_Message
        {
            get
            {
                return _testData_SpecificOutputSource_NonEmpty_Message;
            }
        }

    }
}
