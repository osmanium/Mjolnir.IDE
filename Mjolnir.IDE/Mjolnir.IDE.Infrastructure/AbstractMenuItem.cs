using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Infrastructure
{
    /// <summary>
    /// Class AbstractMenuItem - representation of a Menu
    /// </summary>
    public abstract class AbstractMenuItem : AbstractCommandable, IMenuService
    {
        #region Static

        /// <summary>
        /// The static separator count
        /// </summary>
        protected static int sepCount = 1;

        #endregion

        #region Members

        /// <summary>
        /// The injected container
        /// </summary>
        protected IUnityContainer _container;

        /// <summary>
        /// Is the menu checked
        /// </summary>
        protected bool _isChecked;

        #endregion

        #region Methods and Properties

        /// <summary>
        /// Gets a value indicating whether this instance is separator.
        /// </summary>
        /// <value><c>true</c> if this instance is separator; otherwise, <c>false</c>.</value>
        public virtual bool IsSeparator { get; internal set; }

        /// <summary>
        /// Gets the icon of the menu.
        /// </summary>
        /// <value>The icon.</value>
        public virtual ImageSource Icon { get; internal set; }

        /// <summary>
        /// Gets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        public virtual string ToolTip
        {
            get
            {
                string value = Header.Replace("_", "");
                if (!string.IsNullOrEmpty(InputGestureText))
                {
                    value += " " + InputGestureText;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets the header of the menu.
        /// </summary>
        /// <value>The header.</value>
        public virtual string Header { get; protected internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checkable.
        /// </summary>
        /// <value><c>true</c> if this instance is checkable; otherwise, <c>false</c>.</value>
        public virtual bool IsCheckable { get; protected internal set; }

        /// <summary>
        /// Gets a value indicating whether this instance is visible.
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public virtual bool IsVisible { get; protected internal set; }

        /// <summary>
        /// Gets a value indicating whether to hide this menu item when disabled.
        /// </summary>
        /// <value><c>true</c> if this instance should be hidden when disabled; otherwise, <c>false</c>.</value>
        public virtual bool HideDisabled { get; protected internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public virtual bool IsChecked
        {
            get { return _isChecked; }
            protected internal set
            {
                SetProperty(ref _isChecked, value);
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Header;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        /// <exception cref="System.ArgumentException">Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.</exception>
        public override string Add(AbstractCommandable item)
        {
            if (item.GetType().IsAssignableFrom(typeof(AbstractMenuItem)))
            {
                throw new ArgumentException(
                    "Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.");
            }
            return base.Add(item);
        }

        public virtual void Refresh()
        {
            OnPropertyChanged(() => Header);
            OnPropertyChanged(() => Command);
            OnPropertyChanged(() => Children);
            OnPropertyChanged(() => Icon);
            OnPropertyChanged(() => ToolTip);
            OnPropertyChanged(() => IsVisible);
        }

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMenuItem"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="gesture">The gesture.</param>
        /// <param name="isCheckable">if set to <c>true</c> acts as a checkable menu.</param>
        protected AbstractMenuItem(string header, int priority, ImageSource icon = null, ICommand command = null,
                                   KeyGesture gesture = null, bool isCheckable = false, bool hideDisabled = false)
        {
            Priority = priority;
            IsSeparator = false;
            Header = header;
            Key = header;
            Command = command;
            IsCheckable = isCheckable;
            HideDisabled = hideDisabled;
            Icon = icon;
            if (gesture != null && command != null)
            {
                //Application.Current.MainWindow.InputBindings.Add(new KeyBinding(command, gesture));
                InputGestureText = gesture.DisplayString;
            }
            if (isCheckable)
            {
                IsChecked = false;
            }
            if (Header == "SEP")
            {
                Key = "SEP" + sepCount.ToString();
                Header = "";
                sepCount++;
                IsSeparator = true;
            }
        }

        #endregion
    }
}