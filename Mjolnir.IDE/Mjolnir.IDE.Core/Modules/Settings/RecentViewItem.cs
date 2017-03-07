using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    [Serializable]
    [Browsable(false)]
    public class RecentViewItem : IRecentViewItem
    {
        public RecentViewItem()
        {
            this.DisplayValue = "";
            this.ContentID = "";
        }

        public string DisplayValue { get; set; }
        public string ContentID { get; set; }

        public override bool Equals(object obj)
        {
            RecentViewItem item = obj as RecentViewItem;
            return (item != null) && ContentID.Equals(item.ContentID);
        }

        public override int GetHashCode()
        {
            return ContentID.GetHashCode();
        }

        public override string ToString()
        {
            return ContentID.ToString();
        }
    }
}