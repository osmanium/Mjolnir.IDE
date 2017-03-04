﻿using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Infrastructure.Enums;
using Prism.Events;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Modules.Error.Models;
using Mjolnir.IDE.Modules.Error.Views;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Modules.Error.Interfaces;

namespace Mjolnir.IDE.Modules.Error.ViewModels
{
    public class ErrorViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IUnityContainer _container;
        private readonly ErrorUserControl _view;
        private readonly ObservableCollection<ErrorListItem> _items;
        private readonly IErrorToolboxToolbarService _errorToolbox;



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

        private bool _showErrors = true;
        public bool ShowErrors
        {
            get { return _showErrors; }
            set
            {
                SetProperty(ref _showErrors, value);
                OnPropertyChanged(() => FilteredItems);
            }
        }

        private bool _showWarnings = true;
        public bool ShowWarnings
        {
            get { return _showWarnings; }
            set
            {
                SetProperty(ref _showWarnings, value);
                OnPropertyChanged(() => FilteredItems);
            }
        }

        private bool _showMessages = true;
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

        private static string Pluralize(string singular, string plural, int number)
        {
            if (number == 1)
                return string.Format(singular, number);

            return string.Format(plural, number);
        }


        public ErrorViewModel(IUnityContainer container, AbstractWorkspace workspace, IErrorToolboxToolbarService errorToolbox)
            : base(container, errorToolbox)
        {
            _items = new ObservableCollection<ErrorListItem>();
            _items.CollectionChanged += (sender, e) =>
            {
                OnPropertyChanged(() => FilteredItems);
                OnPropertyChanged(() => ErrorItemCount);
                OnPropertyChanged(() => WarningItemCount);
                OnPropertyChanged(() => MessageItemCount);
            };


            _container = container;
            Name = "Error";
            Title = "Error";
            ContentId = "Error";//TODO : Move to constants
            IsVisible = false;

            _errorToolbox = errorToolbox;

            _view = new ErrorUserControl(this);
            View = _view;
        }

        public void AddItem(ErrorListItemType itemType, string description,
            string path = null, int? line = null, int? column = null,
            System.Action onClick = null)
        {
            Items.Add(new ErrorListItem(itemType, Items.Count + 1, description, path, line, column)
            {
                OnClick = onClick
            });
        }
    }
}
