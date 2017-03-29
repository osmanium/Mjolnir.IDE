using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.UI.Validation;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mjolnir.IDE.Sdk.ViewModels
{
    /// <summary>
    /// The abstract class which has to be inherited if you want to create a document
    /// </summary>
    public abstract class ContentViewModel : ValidatableBindableBase
    {
        #region Members

        /// <summary>
        /// The static count value for "Untitled" number.
        /// </summary>
        protected static int Count = 1;

        /// <summary>
        /// The model
        /// </summary>
        protected ContentModel _model;

        /// <summary>
        /// The command manager
        /// </summary>
        protected ICommandManager _commandManager;

        /// <summary>
        /// The content id of the document
        /// </summary>
        protected string _contentId = null;

        /// <summary>
        /// Is the document active
        /// </summary>
        protected bool _isActive = false;

        /// <summary>
        /// Is the document selected
        /// </summary>
        protected bool _isSelected = false;

        /// <summary>
        /// The logger instance
        /// </summary>
        protected IOutputService _logger;

        /// <summary>
        /// The title of the document
        /// </summary>
        protected string _title = null;

        /// <summary>
        /// The tool tip to display on the document
        /// </summary>
        protected string _tooltip = null;

        /// <summary>
        /// The workspace instance
        /// </summary>
        protected IWorkspace _workspace;

        /// <summary>
        /// The menu service
        /// </summary>
        protected IMenuService _menuService;

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel"/> class.
        /// </summary>
        /// <param name="workspace">The injected workspace.</param>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="logger">The injected logger.</param>
        protected ContentViewModel(DefaultWorkspace workspace, ICommandManager commandManager, IOutputService logger,
                                   IMenuService menuService)
        {
            _workspace = workspace;
            _commandManager = commandManager;
            _logger = logger;
            _menuService = menuService;
            CloseCommand = new DelegateCommand<object>(Close, CanClose);

            IsValidationEnabled = false;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the close command.
        /// </summary>
        /// <value>The close command.</value>
        public virtual ICommand CloseCommand { get; protected internal set; }

        /// <summary>
        /// The content model
        /// </summary>
        /// <value>The model.</value>
        public virtual ContentModel Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                {
                    _model.PropertyChanged -= Model_PropertyChanged;
                }
                if (value != null)
                {
                    _model = value;
                    _model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        /// <summary>
        /// The content view
        /// </summary>
        /// <value>The view.</value>
        public virtual UserControl View { get; set; }

        /// <summary>
        /// The content menu that should be available for the document pane
        /// </summary>
        /// <value>The view.</value>
        public IReadOnlyCollection<DefaultMenuItem> Menus
        {
            get
            {
                DefaultMenuItem item = _menuService.Get("_File").Get("_Save") as DefaultMenuItem;
                List<DefaultMenuItem> items = new List<DefaultMenuItem>();
                items.Add(item);
                return items.AsReadOnly();
            }
        }

        /// <summary>
        /// The title of the document
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title
        {
            get
            {
                if (Model.IsDirty)
                {
                    return _title + "*";
                }
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    SetProperty(ref _title, value);
                }
            }
        }

        /// <summary>
        /// The tool tip of the document
        /// </summary>
        /// <value>The tool tip.</value>
        public virtual string Tooltip
        {
            get { return _tooltip; }
            protected set
            {
                if (_tooltip != value)
                {
                    SetProperty(ref _tooltip, value);
                }
            }
        }

        /// <summary>
        /// The image source that can be used as an icon in the tab
        /// </summary>
        /// <value>The icon source.</value>
        public virtual ImageSource IconSource { get; protected internal set; }

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
        /// <value><c>true</c> if this document is selected; otherwise, <c>false</c>.</value>
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
        /// <value><c>true</c> if this document is active; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// The content handler which does save and load of the file
        /// </summary>
        /// <value>The handler.</value>
        public IContentHandler Handler { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether this instance can close.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if this instance can close; otherwise, <c>false</c>.</returns>
        protected virtual bool CanClose(object obj)
        {
            return (obj != null)
                       ? _commandManager.GetCommand("CLOSE").CanExecute(obj)
                       : _commandManager.GetCommand("CLOSE").CanExecute(this);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        protected virtual void Close(object obj)
        {
            if (obj != null)
            {
                _commandManager.GetCommand("CLOSE").Execute(obj);
            }
            else
            {
                _commandManager.GetCommand("CLOSE").Execute(this);
            }
        }


        protected virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //TODO : Do not uncomment, slows down performance
            //OnPropertyChanged(() => Model);
            //OnPropertyChanged(() => Title);
            //OnPropertyChanged(() => ContentId);
            //OnPropertyChanged(() => Tooltip);
            //OnPropertyChanged(() => IsSelected);
            //OnPropertyChanged(() => IsActive);
        }

        #endregion
    }
}