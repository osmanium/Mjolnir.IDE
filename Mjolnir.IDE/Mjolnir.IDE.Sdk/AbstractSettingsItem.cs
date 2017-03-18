using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Mjolnir.IDE.Sdk
{
    /// <summary>
    /// Class AbstractSettings
    /// </summary>
    public abstract class AbstractSettingsItem : AbstractPrioritizedTree<AbstractSettingsItem>
    {
        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractSettings"/> class.
        /// </summary>
        protected AbstractSettingsItem(string title, AbstractSettings settings) : base()
        {
            this.Title = title;
            this.Key = title;
            this._appSettings = settings;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title of the setting.
        /// </summary>
        /// <value>The title.</value>
        [Browsable(false)]
        public string Title { get; protected set; }

        /// <summary>
        /// Gets the view that displays the setting.
        /// </summary>
        /// <value>The view.</value>
        [Browsable(false)]
        public virtual ContentControl View
        {
            get
            {
                if (_appSettings != null)
                {
                    var p = ContentControl.Content as PropertyGrid;
                    p.SelectedObject = _appSettings;
                    p.SelectedObjectName = "";
                    p.SelectedObjectTypeName = this.Title;
                    return ContentControl;
                }
                if (Children.Count > 0)
                {
                    return Children[0].View;
                }
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets this instance with the default values for settings.
        /// </summary>
        public virtual void Reset()
        {
            foreach (AbstractSettingsItem settings in Children)
            {
                settings.Reset();
            }
            if (_appSettings != null)
            {
                //The app settings should use reload. Reset with set it to "defaults" whereas we need to read from the settings file instead of loading defaults.
                _appSettings.Reload();
            }
        }

        /// <summary>
        /// Saves this instance and the children of the settings. In your own implementation - call base.Save() after you save your settings.
        /// </summary>
        public virtual void Save()
        {
            foreach (AbstractSettingsItem settings in Children)
            {
                settings.Save();
            }
            if (_appSettings != null)
            {
                _appSettings.Save();
            }
        }

        #endregion

        #region Static

        //Singleton which can be reused
        private static readonly ContentControl ContentControl = new ContentControl()
        {
            Content =
                                                                            new PropertyGrid
                                                                            { ShowSearchBox = false }
        };

        #endregion

        #region Members

        protected AbstractSettings _appSettings;

        #endregion
    }
}