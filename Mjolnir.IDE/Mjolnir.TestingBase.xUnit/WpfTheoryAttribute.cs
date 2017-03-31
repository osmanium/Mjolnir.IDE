using System;
using Xunit;
using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
[XunitTestCaseDiscoverer("Mjolnir.TestingBase.xUnit.WpfTheoryDiscoverer", "Mjolnir.TestingBase.xUnit")]
public class WpfTheoryAttribute : TheoryAttribute { }
