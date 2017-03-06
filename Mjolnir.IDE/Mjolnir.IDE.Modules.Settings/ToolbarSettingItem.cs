using Mjolnir.IDE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Settings
{
    [Serializable]
    [Browsable(false)]
    public sealed class ToolbarSettingItem : IToolbar
    {
        public ToolbarSettingItem()
        {
        }

        public ToolbarSettingItem(IToolbar toolbar)
        {
            this.BandIndex = toolbar.BandIndex;
            this.Band = toolbar.Band;
            this.Key = toolbar.Key;
            this.IsChecked = toolbar.IsChecked;
        }

        public int Band { get; set; }

        public int BandIndex { get; set; }

        public string Key { get; set; }

        public bool IsChecked { get; set; }

        public override bool Equals(object obj)
        {
            ToolbarSettingItem item = obj as ToolbarSettingItem;
            return (item != null) && Key.Equals(item.Key);
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }
}