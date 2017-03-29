using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Interfaces.Settings;
using Mjolnir.IDE.Core.Modules.Settings.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    /// <summary>
    /// Class WideSettingsManager
    /// </summary>
    public class SettingsManager : DefaultSettingsItem, ISettingsManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WideSettingsManager"/> class.
        /// </summary>
        public SettingsManager() : base("", null)
        {
            SettingsCommand = new DelegateCommand(OpenSettings);
        }

        /// <summary>
        /// Gets the settings menu.
        /// </summary>
        /// <value>The settings menu.</value>
        public ICommand SettingsCommand { get; private set; }

        private void OpenSettings()
        {
            DefaultSettingsWindow window = new DefaultSettingsWindow();
            window.DataContext = this;
            bool? result = window.ShowDialog();
            if (result == true)
            {
                this.Save();
            }
            else
            {
                this.Reset();
            }
        }
    }
}