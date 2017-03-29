using System;
using Mjolnir.IDE.Test;
using Mjolnir.IDE.Core;
using Microsoft.Practices.Unity;
using Xunit;

namespace Mjolnir.IDE.Core.UnitTests
{
    public class WorkSpaceUnitTests
    {
        [Fact]
        public void Should_load_workspace()
        {
            Test.App app = new Test.App();


            var container = app.Bootstrapper.Container;

            Workspace workspace = container.Resolve<Workspace>();


        }
    }
}
