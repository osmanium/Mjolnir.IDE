﻿using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Infrastructure.Enums;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Prism.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Core.Modules.Toolbox.Views;
using System.Windows.Data;
using Mjolnir.IDE.Infrastructure.Attributes;
using System.Reflection;
using Mjolnir.IDE.Infrastructure.Events;
using System.Collections.ObjectModel;

namespace Mjolnir.IDE.Core.Modules.Toolbox.ViewModels
{
    public class ToolboxViewModel : ToolViewModel, IToolboxService
    {
        public override PaneLocation PreferredLocation => PaneLocation.Left;

        private readonly IUnityContainer _container;
        private readonly ToolboxUserControl _view;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWorkspace _workspace;


        private readonly Dictionary<Type, IEnumerable<ToolboxItem>> _toolboxItems;


        private readonly ObservableCollection<ToolboxItemViewModel> _items;
        public ObservableCollection<ToolboxItemViewModel> Items
        {
            get { return _items; }
        }


        public ToolboxViewModel(IUnityContainer container,
                              AbstractWorkspace workspace,
                              IEventAggregator eventAggregator,
                              IToolboxToolbarService toolboxToolbarService
                              )
            : base(container, toolboxToolbarService)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _workspace = workspace;


            Name = "Toolbox";
            Title = "Toolbox";
            ContentId = "Toolbox";//TODO : Move to constants
            IsVisible = false;


            _view = new ToolboxUserControl(this);
            View = _view;


            _items = new ObservableCollection<ToolboxItemViewModel>();

            var groupedItems = CollectionViewSource.GetDefaultView(_items);
            groupedItems.GroupDescriptions.Add(new PropertyGroupDescription("Category"));


            //TODO : !!!This should check specific folder to scan assemblies for toolbox items
            _toolboxItems = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(y => y.GetCustomAttributes<ToolboxItemAttribute>(true).Any())
                .Select(x =>
                {
                    var attribute = x.GetCustomAttributes<ToolboxItemAttribute>(true).First();
                    return new ToolboxItem
                    {
                        DocumentType = attribute.DocumentType,
                        Name = attribute.Name,
                        Category = attribute.Category,
                        IconSource = (attribute.IconSource != null) ? new Uri(attribute.IconSource) : null,
                        ItemType = x,
                    };
                })
                .GroupBy(x => x.DocumentType)
                .ToDictionary(x => x.Key, x => x.AsEnumerable());

            RefreshToolboxItems();

            _eventAggregator.GetEvent<ActiveContentChangedEvent>().Subscribe(ActiveContentChanged);
        }

        private void ActiveContentChanged(ContentViewModel viewModel)
        {
            RefreshToolboxItems();
        }

        

        private void RefreshToolboxItems()
        {
            _items.Clear();

            if (_workspace.ActiveDocument == null)
                return;

            _items.AddRange(GetToolboxItems(_workspace.ActiveDocument.GetType())
                .Select(x => new ToolboxItemViewModel(x)));


            OnPropertyChanged(() => Items);
        }

        public IEnumerable<ToolboxItem> GetToolboxItems(Type documentType)
        {
            IEnumerable<ToolboxItem> result;
            if (_toolboxItems.TryGetValue(documentType, out result))
                return result;
            return new ObservableCollection<ToolboxItem>();
        }
    }
}
