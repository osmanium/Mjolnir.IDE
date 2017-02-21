using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Settings
{
    public class ThemeSettings : AbstractSettings, IThemeSettings
    {
        public ThemeSettings(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ThemeChangeEvent>().Subscribe(NewSelectedTheme);
        }

        private void NewSelectedTheme(ITheme theme)
        {
            this.SelectedTheme = theme.Name;
            this.Save();
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Dark")]
        public string SelectedTheme
        {
            get { return (string)this["SelectedTheme"]; }
            set { this["SelectedTheme"] = value; }
        }
    }
}