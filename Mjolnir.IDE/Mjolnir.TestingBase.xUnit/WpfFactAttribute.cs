using System;
using Xunit;
using Xunit.Sdk;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
[XunitTestCaseDiscoverer("Mjolnir.TestingBase.xUnit.WpfFactDiscoverer", "Mjolnir.TestingBase.xUnit")]
public class WpfFactAttribute : FactAttribute { }
