using Mjolnir.IDE.Sdk.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Events
{
    /// <summary>
    /// Class ThemeChangeEvent - This event happens when a theme is changed.
    /// </summary>
    public class ThemeChangeEvent : PubSubEvent<ITheme>
    {
    }
}
