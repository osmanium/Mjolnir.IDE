﻿using Mjolnir.IDE.Infrastructure.Interfaces;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Events
{
    /// <summary>
    /// Class ThemeChangeEvent - This event happens when a theme is changed.
    /// </summary>
    public class ThemeChangeEvent : PubSubEvent<ITheme>
    {
    }
}
