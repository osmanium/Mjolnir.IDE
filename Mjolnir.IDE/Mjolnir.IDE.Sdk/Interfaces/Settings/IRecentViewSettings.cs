using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mjolnir.IDE.Sdk.Interfaces.Settings
{
    public interface IRecentViewSettings
    {
        [XmlIgnore]
        AbstractMenuItem RecentMenu { get; }

        [XmlIgnore]
        IReadOnlyList<IRecentViewItem> RecentItems { get; }
    }
}