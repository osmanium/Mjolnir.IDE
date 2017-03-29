using Mjolnir.IDE.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    public class MjolnirSettingsItem : DefaultSettingsItem
    {
        public MjolnirSettingsItem(string title, int priority, DefaultSettings settings) 
            : base(title, settings)
        {
            this.Priority = priority;
        }
    }
}