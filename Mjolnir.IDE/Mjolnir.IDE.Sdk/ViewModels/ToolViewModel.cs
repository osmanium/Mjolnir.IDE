using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk.ViewModels
{
    /// <summary>
    /// The abstract class which has to be inherited if you want to create a tool
    /// </summary>
    public abstract class ToolViewModel : ValidatableBindableBase, ITool
    {
        #region Members

        protected string _contentId = null;
        protected bool _isActive = false;
        protected bool _isSelected = false;
        private bool _isVisible = false;
        protected string _title = null;


        #endregion

        #region Property

        /// <summary>
        /// The name of the tool
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The visibility of the tool
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    SetProperty(ref _isVisible, value);
                }
            }
        }


        /// <summary>
        /// pane location
        /// </summary>
        public abstract PaneLocation PreferredLocation { get; }

        /// <summary>
        /// Prefered width
        /// </summary>
        public virtual double PreferredWidth
        {
            get { return 200; }
        }

        /// <summary>
        /// prefered height
        /// </summary>
        public virtual double PreferredHeight
        {
            get { return 200; }
        }


        /// <summary>
        /// The content model
        /// </summary>
        /// <value>The model.</value>
        //public virtual ValidatableBindableBase Model { get; set; }

        /// <summary>
        /// The content view
        /// </summary>
        /// <value>The view.</value>
        public virtual IContentView View { get; set; }

        /// <summary>
        /// The title of the document
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    SetProperty(ref _title, value);
                }
            }
        }

        public IReadOnlyList<string> Menus
        {
            get { return new List<string>() { "a", "b", "c" }; }
        }

        /// <summary>
        /// The image source that can be used as an icon in the tab
        /// </summary>
        /// <value>The icon source.</value>
        public virtual ImageSource IconSource { get; protected set; }

        /// <summary>
        /// The content ID - unique value for each document
        /// </summary>
        /// <value>The content id.</value>
        public virtual string ContentId
        {
            get { return _contentId; }
            protected set
            {
                if (_contentId != value)
                {
                    SetProperty(ref _contentId, value);
                }
            }
        }

        /// <summary>
        /// Is the document selected
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    SetProperty(ref _isSelected, value);
                }
            }
        }

        /// <summary>
        /// Is the document active
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public virtual bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    SetProperty(ref _isActive, value);
                }
            }
        }
        

        public ToolBarTray ToolBarTray
        {
            get { return (_toolbarService as IToolbarServiceBase).ToolBarTray; }
        }


        private IToolboxToolbar _toolbarService;
        #endregion

        public ToolViewModel(IToolbarServiceBase toolbarService)
        {
            _toolbarService = toolbarService as DefaultToolbar;
            IsValidationEnabled = false;
        }
    }
}