using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Settings
{
    public interface IRecentViewItem
    {
        string ContentID { get; }
        string DisplayValue { get; }
    }
}