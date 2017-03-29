using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk
{
    /// <summary>
    /// Class AbstractMenuItem - representation of a Menu
    /// </summary>
    public class DefaultMenuItem : Commandable, IMenuService
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
                string value = Key.Replace("_", "");
                if (!string.IsNullOrEmpty(InputGestureText))
                {
                    value += " " + InputGestureText;
                }
                return value;
            }
        }


        private string _text;
        public virtual string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checkable.
        /// </summary>
        /// <value><c>true</c> if this instance is checkable; otherwise, <c>false</c>.</value>
        public virtual bool IsCheckable { get; set; }

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

        public bool IsToggleButton { get; set; }

        public bool IsSplitButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public virtual bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                SetProperty(ref _isChecked, value);
            }
        }

        //private object _tag;
        //public object Tag
        //{
        //    get { return _tag; }
        //    set { SetProperty(ref _tag, value); }
        //}

        #endregion

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        /// <exception cref="System.ArgumentException">Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.</exception>
        public override string Add(Commandable item)
        {
            if (item.GetType().IsAssignableFrom(typeof(DefaultMenuItem)))
            {
                throw new ArgumentException(
                    "Expected a AbstractMenuItem as the argument. Only Menu's can be added within a Menu.");
            }
            return base.Add(item);
        }

        public virtual void Refresh()
        {
            OnPropertyChanged(() => Key);
            OnPropertyChanged(() => Command);
            OnPropertyChanged(() => Children);
            OnPropertyChanged(() => Icon);
            OnPropertyChanged(() => ToolTip);
            OnPropertyChanged(() => IsVisible);
        }

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMenuItem"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="gesture">The gesture.</param>
        /// <param name="isCheckable">if set to <c>true</c> acts as a checkable menu.</param>
        protected DefaultMenuItem(string key, string text, int priority, ImageSource icon = null, ICommand command = null,
                                   KeyGesture gesture = null, bool isCheckable = false, bool hideDisabled = false,
                                   bool isToggleButton = false, bool isSplitButton = true)
        {
            Text = text;
            IsToggleButton = isToggleButton;
            IsSplitButton = isSplitButton;
            Priority = priority;
            IsSeparator = false;
            Key = key;
            base.Key = key;
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
            if (Key == "SEP")
            {
                base.Key = "SEP" + sepCount.ToString();
                Key = "";
                sepCount++;
                IsSeparator = true;
            }
        }

        #endregion
    }
}