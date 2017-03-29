using Mjolnir.IDE.Core.Modules.ErrorList.Events;
using Mjolnir.IDE.Core.Modules.ErrorList.ViewModels;
using Mjolnir.IDE.Modules.Error.Interfaces;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.UnitTests
{
    public class ViewModelTestBase
    {
        protected Mock<IApplicationDefinition> _applicationDefinition;
        protected Mock<IMenuService> _menuService;
        protected Mock<IShellToolbarService> _shellToolbarService;
        protected Mock<IStatusbarService> _statusbarService;
        protected Mock<ICommandManager> _commandManager;
        protected Mock<DefaultWorkspace> _workspace;
        protected Mock<IEventAggregator> _eventAggregator;


        public ViewModelTestBase()
        {
            _eventAggregator = new Mock<IEventAggregator>();
            _applicationDefinition = new Mock<IApplicationDefinition>();
            _menuService = new Mock<IMenuService>();
            _shellToolbarService = new Mock<IShellToolbarService>();
            _statusbarService = new Mock<IStatusbarService>();
            _commandManager = new Mock<ICommandManager>();
            
            _workspace = new Mock<DefaultWorkspace>(MockBehavior.Loose, _eventAggregator.Object, _applicationDefinition.Object, _menuService.Object, _shellToolbarService.Object, _statusbarService.Object, _commandManager.Object);
        }



    }
}
