using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Shell
{
    public class MjolnirApp : Application
    {
        public MjolnirBootstrapper Bootstrapper { get; set; }

        public MjolnirApp()
        {
            Bootstrapper = new MjolnirBootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
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
