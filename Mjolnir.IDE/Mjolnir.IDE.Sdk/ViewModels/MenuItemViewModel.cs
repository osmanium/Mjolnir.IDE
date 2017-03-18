using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk.Interfaces.ViewModels
{
    /// <summary>
    /// Class MenuItemViewModel - simple menu implementation which can be reused by apps
    /// </summary>
    public sealed class MenuItemViewModel : AbstractMenuItem
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemViewModel"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="command">The command.</param>
        /// <param name="gesture">The gesture.</param>
        /// <param name="isCheckable">if set to <c>true</c> this menu acts as a checkable menu.</param>
        /// <param name="hideDisabled">if set to <c>true</c> this menu is not visible when disabled.</param>
        /// <param name="container">The container.</param>
        public MenuItemViewModel(string key, string text, int priority, ImageSource icon = null, ICommand command = null,
                                 KeyGesture gesture = null, bool isCheckable = false, bool hideDisabled = false,
                                 IUnityContainer container = null, bool isToggleButton = false, bool isSplitButton = true)
            : base(key, text, priority, icon, command, gesture, isCheckable, hideDisabled, isToggleButton, isSplitButton)
        {
        }

        #endregion

        #region Static

        /// <summary>
        /// Creates an instance of a menu acting as a separator with a priority
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <returns>AbstractMenuItem.</returns>
        public static AbstractMenuItem Separator(int priority)
        {
            return new MenuItemViewModel("SEP", string.Empty, priority);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checkable.
        /// </summary>
        /// <value><c>true</c> if this instance is checkable; otherwise, <c>false</c>.</value>
        public new bool IsCheckable
        {
            get { return base.IsCheckable; }
            set { base.IsCheckable = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public new bool IsChecked
        {
            get { return base.IsChecked; }
            set { base.IsChecked = value; }
        }

        #endregion
    }
}