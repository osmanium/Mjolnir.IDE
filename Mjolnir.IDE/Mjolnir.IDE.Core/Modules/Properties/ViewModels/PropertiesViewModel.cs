using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Enums;
using Prism.Events;
using Mjolnir.IDE.Core.Modules.Properties.Views;

namespace Mjolnir.IDE.Core.Modules.Properties.ViewModels
{
    public class PropertiesViewModel : ToolViewModel, IPropertyGrid
    {
        private readonly IEventAggregator _aggregator;
        private readonly IUnityContainer _container;
        private readonly PropertiesView _view;
        private readonly IPropertiesToolboxToolbarService _propertiesToolboxToolbar;
        private readonly ICommandManager _commandManager;


        private object _selectedObject;
        public object SelectedObject
        {
            get{ return _selectedObject; }
            set { SetProperty(ref _selectedObject, value); }
        }

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        public PropertiesViewModel(IUnityContainer container, 
                                   IPropertiesToolboxToolbarService toolbarService,
                                   IEventAggregator aggregator,
                                   ICommandManager commandManager) 
            : base(container, toolbarService)
        {
            IsValidationEnabled = false;

            _container = container;
            _propertiesToolboxToolbar = toolbarService;
            _aggregator = aggregator;
            _commandManager = commandManager;
            

            Name = "Properties";
            Title = "Properties";
            ContentId = "Properties";//TODO : Move to constants
            IsVisible = false;


            _view = new PropertiesView(this);
            View = _view;
            
        }

        
    }
}
