using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    public class WindowPositionSettings : AbstractSettings, IWindowPositionSettings
    {
        public WindowPositionSettings(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<WindowClosingEvent>().Subscribe(SaveWindowPositions);
        }

        private void SaveWindowPositions(Window window)
        {
            if (window.WindowState == WindowState.Normal)
            {
                Left = window.Left;
                Top = window.Top;
                Height = window.Height;
                Width = window.Width;
            }
            State = window.WindowState;
            Save();
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public double Left
        {
            get { return (double)this["Left"]; }
            set { this["Left"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public double Top
        {
            get { return (double)this["Top"]; }
            set { this["Top"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("600")]
        public double Height
        {
            get { return (double)this["Height"]; }
            set { this["Height"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("800")]
        public double Width
        {
            get { return (double)this["Width"]; }
            set { this["Width"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Minimized")]
        public WindowState State
        {
            get { return (WindowState)this["State"]; }
            set { this["State"] = value; }
        }
    }
}