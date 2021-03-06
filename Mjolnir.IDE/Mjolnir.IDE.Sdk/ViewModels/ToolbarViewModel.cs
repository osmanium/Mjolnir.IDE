﻿using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk.ViewModels
{
    /// <summary>
    /// Class ToolbarViewModel
    /// </summary>
    public sealed class ToolbarViewModel : DefaultToolbar
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolbarViewModel"/> class.
        /// </summary>
        /// <param name="key">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="isCheckable">if set to <c>true</c> does nothing in the case of toolbar - default value is false.</param>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentException">Header cannot be SEP for a Toolbar</exception>
        public ToolbarViewModel(string key, string text, int priority, ImageSource icon = null, ICommand command = null,
                                bool isCheckable = false, IUnityContainer container = null)
            : base(key, text, priority, icon, command, isCheckable, container)
        {
        }

        #endregion
    }
}