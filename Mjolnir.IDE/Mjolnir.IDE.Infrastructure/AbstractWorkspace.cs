using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mjolnir.IDE.Infrastructure
{
    /// <summary>
    /// Class AbstractWorkspace
    /// </summary>
    public abstract class AbstractWorkspace : ValidatableBindableBase, IWorkspace
    {
        #region Fields

        /// <summary>
        /// The injected container
        /// </summary>
        protected readonly IUnityContainer _container;

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        protected readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The active document
        /// </summary>
        private ContentViewModel _activeDocument;

        /// <summary>
        /// The injected command manager
        /// </summary>
        protected ICommandManager _commandManager;

        /// <summary>
        /// The list of documents
        /// </summary>
        protected ObservableCollection<ContentViewModel> _docs = new ObservableCollection<ContentViewModel>();

        /// <summary>
        /// The menu service
        /// </summary>
        protected MenuItemViewModel _menus;

        /// <summary>
        /// The toolbar service
        /// </summary>
        protected AbstractToolbar _toolbarService;

        /// <summary>
        /// The status bar service
        /// </summary>
        protected IStatusbarService _statusbarService;

        /// <summary>
        /// The list of tools
        /// </summary>
        protected ObservableCollection<ToolViewModel> _tools = new ObservableCollection<ToolViewModel>();

        private IApplicationDefinition _applicationDefinition;

        public ImageSource Icon
        {
            get
            {
                return _applicationDefinition.ApplicationIconSource;
            }
        }

        public string Title
        {
            get
            {
                return _applicationDefinition.ApplicationName;
            }
        }

        #endregion

        #region CTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractWorkspace" /> class.
        /// </summary>
        /// <param name="container">The injected container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        protected AbstractWorkspace(IUnityContainer container, IEventAggregator eventAggregator, IApplicationDefinition applicationDefinition)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _applicationDefinition = applicationDefinition;
            _docs = new ObservableCollection<ContentViewModel>();
            _docs.CollectionChanged += Docs_CollectionChanged;
            _tools = new ObservableCollection<ToolViewModel>();
            _menus = _container.Resolve<IMenuService>() as MenuItemViewModel;
            _menus.PropertyChanged += _menus_PropertyChanged;
            _toolbarService = _container.Resolve<IToolbarService>() as AbstractToolbar;
            _statusbarService = _container.Resolve<IStatusbarService>();
            _commandManager = _container.Resolve<ICommandManager>();

            IsValidationEnabled = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>The menu.</value>
        public IList<AbstractCommandable> Menus
        {
            get { return _menus.Children; }
        }

        /// <summary>
        /// Gets the tool bar tray.
        /// </summary>
        /// <value>The tool bar tray.</value>
        public ToolBarTray ToolBarTray
        {
            get { return (_toolbarService as IToolbarService).ToolBarTray; }
        }

        public IStatusbarService StatusBar
        {
            get { return _statusbarService; }
        }

        #endregion

        #region IWorkspace Members

        /// <summary>
        /// The list of documents which are open in the workspace
        /// </summary>
        /// <value>The documents.</value>
        public virtual ObservableCollection<ContentViewModel> Documents
        {
            get { return _docs; }
            set { SetProperty(ref _docs, value); }
        }

        /// <summary>
        /// The list of tools that are available in the workspace
        /// </summary>
        /// <value>The tools.</value>
        public virtual ObservableCollection<ToolViewModel> Tools
        {
            get { return _tools; }
            set { SetProperty(ref _tools, value); }
        }

        /// <summary>
        /// The current document which is active in the workspace
        /// </summary>
        /// <value>The active document.</value>
        public virtual ContentViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                if (_activeDocument != value)
                {
                    SetProperty(ref _activeDocument, value);
                    _commandManager.Refresh();
                    _menus.Refresh();
                    _eventAggregator.GetEvent<ActiveContentChangedEvent>().Publish(_activeDocument);
                }
            }
        }

        
        /// <summary>
        /// Closing this instance.
        /// </summary>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        /// <returns><c>true</c> if the application is closing, <c>false</c> otherwise</returns>
        public virtual bool Closing(CancelEventArgs e)
        {
            for (int i = 0; i < Documents.Count; i++)
            {
                ContentViewModel vm = Documents[i];
                if (vm.Model.IsDirty)
                {
                    ActiveDocument = vm;

                    //Execute the close command
                    vm.CloseCommand.Execute(e);

                    //If canceled
                    if (e.Cancel == true)
                    {
                        return false;
                    }

                    //If it was a new view model with no location to save, we have removed the view model from documents - so reduce the count
                    if (vm.Model.Location == null)
                    {
                        i--;
                    }
                }
            }
            return true;
        }

        #endregion

        /// <summary>
        /// Handles the PropertyChanged event of the menu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void _menus_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(() => Menus);
        }


        protected void Docs_CollectionChanged(object sender,
                                              System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= ModelChangedEventHandler;
            }

            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += ModelChangedEventHandler;
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (_docs.Count == 0)
                {
                    this.ActiveDocument = null;
                }
            }
        }

        /// <summary>
        /// The changed event handler when a property on the model changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void ModelChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            _commandManager.Refresh();
            _menus.Refresh();
            _toolbarService.Refresh();
        }
    }
}
