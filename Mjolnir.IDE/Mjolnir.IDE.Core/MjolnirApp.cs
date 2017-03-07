using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;

namespace Mjolnir.IDE.Core
{
    public abstract class MjolnirApp : Application
    {
        public MjolnirBootstrapper Bootstrapper { get; set; }

        public IApplicationDefinition ApplicationDefinition { get; set; }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrapper = new MjolnirBootstrapper(ApplicationDefinition);
            base.OnStartup(e);
            Bootstrapper.Run();
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            GC.Collect();
            base.OnExit(e);
        }
    }
}
