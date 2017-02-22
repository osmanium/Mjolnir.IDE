using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mjolnir.IDE.Shell.ViewModel
{
    public class ShellViewModel : ValidatableBindableBase
    {
        private readonly IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private ILoggerService _logger;
        private IWorkspace _workspace;
        private ContextMenu _docContextMenu;
        private MultiBinding _itemSourceBinding;
        private AbstractWorkspace _abstractWorkspace;

        public AbstractWorkspace Workspace
        {
            get { return _abstractWorkspace; }
            set
            {
                SetProperty(ref _abstractWorkspace, value);
            }
        }

        public ShellViewModel(IUnityContainer container,
                              IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }
    }
}
