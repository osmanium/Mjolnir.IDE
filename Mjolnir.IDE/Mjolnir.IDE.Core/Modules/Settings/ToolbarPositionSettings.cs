﻿using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Interfaces.Settings;
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
    public class ToolbarPositionSettings : DefaultSettings, IToolbarPositionSettings
    {
        private DefaultToolbar _toolTray;
        private Dictionary<string, ToolbarSettingItem> _loadDict;

        public ToolbarPositionSettings(IEventAggregator eventAggregator, IShellToolbarService toolbarService)
        {
            eventAggregator.GetEvent<WindowClosingEvent>().Subscribe(SaveToolbarPositions);
            _toolTray = toolbarService as DefaultToolbar;
            _loadDict = new Dictionary<string, ToolbarSettingItem>();

            if (this.Toolbars != null && this.Toolbars.Count > 0)
            {
                foreach (var setting in this.Toolbars)
                {
                    _loadDict[setting.Key] = setting;
                }

                for (int i = 0; i < _toolTray.Children.Count; i++)
                {
                    DefaultToolbar tb = _toolTray.Children[i] as DefaultToolbar;
                    if (_loadDict.ContainsKey(tb.Key))
                    {
                        ToolbarSettingItem item = _loadDict[tb.Key];
                        tb.Band = item.Band;
                        tb.BandIndex = item.BandIndex;
                        tb.IsChecked = item.IsChecked;
                        tb.Refresh();
                    }
                }
            }
        }

        private void SaveToolbarPositions(Window window)
        {
            this.Toolbars = new List<ToolbarSettingItem>();
            for (int i = 0; i < _toolTray.Children.Count; i++)
            {
                DefaultToolbar tb = _toolTray.Children[i] as DefaultToolbar;
                Toolbars.Add(new ToolbarSettingItem(tb));
            }
            Save();
        }

        [UserScopedSetting()]
        public List<ToolbarSettingItem> Toolbars
        {
            get
            {
                if ((List<ToolbarSettingItem>)this["Toolbars"] == null)
                    this["Toolbars"] = new List<ToolbarSettingItem>();
                return (List<ToolbarSettingItem>)this["Toolbars"];
            }
            set { this["Toolbars"] = value; }
        }
    }
}