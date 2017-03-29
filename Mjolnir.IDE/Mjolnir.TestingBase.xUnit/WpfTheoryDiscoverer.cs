using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Mjolnir.TestingBase.xUnit
{
    public class WpfTheoryDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly TheoryDiscoverer theoryDiscoverer;

        public WpfTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        {
            theoryDiscoverer = new TheoryDiscoverer(diagnosticMessageSink);
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return theoryDiscoverer.Discover(discoveryOptions, testMethod, factAttribute)
                                   .Select(testCase => new WpfTestCase(testCase));
        }
    }
}
