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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Core
{
    public abstract class MjolnirApp : Application, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MjolnirBootstrapper Bootstrapper { get; set; }

        private string _applicationName;
        public virtual string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private ImageSource _applicationIconSource;
        public virtual ImageSource ApplicationIconSource
        {
            get { return _applicationIconSource; }
            set { _applicationIconSource = value; }
        }


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
            //Application Defaults
            _applicationName = "Mjolnir.IDE";
            _applicationIconSource = new BitmapImage(new Uri(@"pack://application:,,,/Mjolnir.IDE.Core;component/Assets/Mjolnir_Icon.png"));

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
