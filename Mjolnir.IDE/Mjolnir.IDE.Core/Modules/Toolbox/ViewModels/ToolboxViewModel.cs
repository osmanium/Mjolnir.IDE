using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Sdk.Enums;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk;
using Prism.Events;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Core.Modules.Toolbox.Views;
using System.Windows.Data;
using Mjolnir.IDE.Sdk.Attributes;
using System.Reflection;
using Mjolnir.IDE.Sdk.Events;
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


        private readonly Dictionary<string, List<ToolboxItem>> _toolboxItems;


        private readonly ObservableCollection<ToolboxItemViewModel> _items;
        public ObservableCollection<ToolboxItemViewModel> Items
        {
            get { return _items; }
        }


        public ToolboxViewModel(DefaultWorkspace workspace,
                              IEventAggregator eventAggregator,
                              IToolboxToolbarService toolboxToolbarService
                              )
            : base(toolboxToolbarService)
        {
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
                .ToDictionary(x => x.Key.Name, x => x.AsEnumerable().ToList());

            RefreshToolboxItems();

            _eventAggregator.GetEvent<ActiveContentChangedEvent>().Subscribe(ActiveContentChanged);
        }

        private void ActiveContentChanged(ContentViewModel viewModel)
        {
            RefreshToolboxItems();
        }



        public void RefreshToolboxItems()
        {
            _items.Clear();

            if (_workspace.ActiveDocument == null)
                return;

            _items.AddRange(GetToolboxItems(_workspace.ActiveDocument.GetType().Name)
                .Select(x => new ToolboxItemViewModel(x)));


            OnPropertyChanged(() => Items);
        }

        public List<ToolboxItem> GetToolboxItems(string documentTypeName)
        {
            List<ToolboxItem> result;
            if (_toolboxItems.TryGetValue(documentTypeName, out result))
                return result;
            return new List<ToolboxItem>();
        }

        public void AddToolboxItems(string documentTypeName, List<ToolboxItem> newItems)
        {
            if (!_toolboxItems.ContainsKey(documentTypeName))
                _toolboxItems[documentTypeName] = newItems;
            else
            {
                var existingItems = _toolboxItems[documentTypeName] as List<ToolboxItem>;

                if (existingItems != null)
                {
                    existingItems.AddRange(newItems.Where(w => !existingItems.Contains(w)));
                    _toolboxItems[documentTypeName] = existingItems;
                }
            }

            RefreshToolboxItems();
        }

        public void RemoveToolboxItems(string documentTypeName, List<ToolboxItem> removedItems)
        {
            if (_toolboxItems.ContainsKey(documentTypeName))
            {
                var existingItems = _toolboxItems[documentTypeName];

                if (existingItems != null)
                {

                    var itemsToBeDeleted = _toolboxItems[documentTypeName].Where(w => w.DocumentType.Name == documentTypeName && removedItems.Where(r => r.Name == w.Name).Any()).ToList();

                    if (itemsToBeDeleted != null)
                        itemsToBeDeleted.ForEach(f =>
                        {
                            _toolboxItems[documentTypeName].Remove(f);
                        });

                }
            }

            RefreshToolboxItems();
        }
    }
}
