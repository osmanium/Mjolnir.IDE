using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mjolnir.IDE.Test;
using Mjolnir.IDE.Core;
using Microsoft.Practices.Unity;

namespace Mjolnir.IDE.UnitTests
{
    [TestClass]
    public class WorkSpaceUnitTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Test.App app = new Test.App();


            var container = app.Bootstrapper.Container;

            Workspace workspace = container.Resolve<Workspace>();


        }
    }
}
