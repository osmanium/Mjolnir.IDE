using Mjolnir.IDE.Sdk.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces;
using System.Windows.Media;

namespace Mjolnir.IDE.Core
{
    public abstract class MjolnirApp : Application
    {
        public MjolnirBootstrapper Bootstrapper { get; set; }

        public abstract string ApplicationName { get; set; }
        public abstract ImageSource ApplicationIconSource { get; set; }
        
        
        public abstract void InitalizeIDE();
        public abstract void RegisterTypes();
        public abstract void LoadTheme();
        public abstract void LoadMenus();
        public abstract void LoadToolbar();
        public abstract void LoadSettings();
        public abstract void LoadCommands();
        public abstract void LoadModules();

        protected override void OnStartup(StartupEventArgs e)
        {
            Bootstrapper = new MjolnirBootstrapper(this);
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
