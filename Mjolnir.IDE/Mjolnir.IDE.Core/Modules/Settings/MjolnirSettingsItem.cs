using Mjolnir.IDE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    public class MjolnirSettingsItem : AbstractSettingsItem
    {
        public MjolnirSettingsItem(string title, int priority, AbstractSettings settings) 
            : base(title, settings)
        {
            this.Priority = priority;
        }
    }
}