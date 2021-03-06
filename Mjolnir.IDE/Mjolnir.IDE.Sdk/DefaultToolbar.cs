﻿using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk
{
    public class DefaultToolbar : DefaultMenuItem, IToolbar, IShellToolbar, IToolboxToolbar
    {
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
        protected DefaultToolbar(string key, string text, int priority, ImageSource icon = null, ICommand command = null,
                                  bool isCheckable = false, IUnityContainer container = null, bool isToggleButton = false, bool isSplitButton = true)
            : base(key, text, priority, icon, command, null, isCheckable, false, isToggleButton, isSplitButton)
        {
            Text = text;
            IsToggleButton = isToggleButton;
            IsSplitButton = isSplitButton;
            Priority = priority;
            IsSeparator = false;
            Key = key;
            Command = command;
            IsCheckable = isCheckable;
            Icon = icon;
            if (isCheckable)
            {
                IsChecked = false;
            }
            if (Key == "SEP")
            {
                throw new ArgumentException("Header cannot be SEP for a Toolbar");
            }
        }

        /// <summary>
        /// Gets or sets the band number for the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The band.</value>
        public int Band { get; set; }

        /// <summary>
        /// Gets or sets the band index of the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The index of the band.</value>
        public int BandIndex { get; set; }

        public override void Refresh()
        {
            OnPropertyChanged(() => Band);
            OnPropertyChanged(() => BandIndex);
            base.Refresh();
        }
    }
}