using Mjolnir.IDE.Sdk.ViewModels;
using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Sdk.Enums;
using Prism.Events;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.ErrorList.Views;
using Mjolnir.IDE.Sdk;
using System.Collections.Specialized;
using Mjolnir.IDE.Modules.Error.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Core.Modules.ErrorList.Events;

namespace Mjolnir.IDE.Core.Modules.ErrorList.ViewModels
{
    public class ErrorViewModel : ToolViewModel, IErrorService
    {
        #region Fields
        private readonly ErrorUserControl _view;
        private readonly ObservableCollection<ErrorListItem> _items;
        private readonly IErrorToolboxToolbarService _errorToolbox;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Properties
        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }

        public ObservableCollection<ErrorListItem> Items
        {
            get { return _items; }
        }

        public IEnumerable<ErrorListItem> FilteredItems
        {
            get
            {
                var items = _items.AsEnumerable();
                if (!ShowErrors)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Error);
                if (!ShowWarnings)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Warning);
                if (!ShowMessages)
                    items = items.Where(x => x.ItemType != ErrorListItemType.Message);
                return items;
            }
        }

        private bool _showErrors = false;
        public bool ShowErrors
        {
            get { return _showErrors; }
            set
            {
                SetProperty(ref _showErrors, value);
                OnPropertyChanged(() => FilteredItems);
            }
        }

        private bool _showWarnings = false;
        public bool ShowWarnings
        {
            get { return _showWarnings; }
            set
            {
                SetProperty(ref _showWarnings, value);
                OnPropertyChanged(() => FilteredItems);
            }
        }

        private bool _showMessages = false;
        public bool ShowMessages
        {
            get { return _showMessages; }
            set
            {
                SetProperty(ref _showMessages, value);
                OnPropertyChanged(() => FilteredItems);
            }
        }


        public int ErrorItemCount
        {
            get { return _items.Count(x => x.ItemType == ErrorListItemType.Error); }
        }

        public int WarningItemCount
        {
            get { return _items.Count(x => x.ItemType == ErrorListItemType.Warning); }
        }

        public int MessageItemCount
        {
            get { return _items.Count(x => x.ItemType == ErrorListItemType.Message); }
        }

        #endregion

        #region Constructors
        public ErrorViewModel(DefaultWorkspace workspace,
                              IErrorToolboxToolbarService errorToolbox,
                              IEventAggregator eventAggregator)
            : base(errorToolbox)
        {
            _eventAggregator = eventAggregator;
            _errorToolbox = errorToolbox;

            _items = new ObservableCollection<ErrorListItem>();
            _items.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged(() => FilteredItems);
                OnPropertyChanged(() => ErrorItemCount);
                OnPropertyChanged(() => WarningItemCount);
                OnPropertyChanged(() => MessageItemCount);

                _eventAggregator.GetEvent<ErrorListUpdated>().Publish(new ErrorListUpdated());
            };


            Name = "Error";
            Title = "Error";
            ContentId = "Error";//TODO : Move to constants
            IsVisible = false;


            _view = new ErrorUserControl(this);
            View = _view;
        }

        #endregion

        #region Public Methods
        public void LogError(ErrorListItem error)
        {
            Items.Add(error);
        }

        public void RemoveError(Guid errorId)
        {
            var itemToBeRemoved = Items.Where(w => w.Id == errorId).FirstOrDefault();

            if (itemToBeRemoved != null)
                Items.Remove(itemToBeRemoved);
        }
        #endregion

        #region Private Methods
        private static string Pluralize(string singular, string plural, int number)
        {
            if (number == 1)
                return string.Format(singular, number);

            return string.Format(plural, number);
        }
        #endregion
    }
}
