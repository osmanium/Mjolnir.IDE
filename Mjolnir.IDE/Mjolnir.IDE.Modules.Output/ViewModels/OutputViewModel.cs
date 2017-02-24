using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Enums;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Output.ViewModels
{
    public  class OutputViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IUnityContainer _container;
        private string _text;

        public string Text
        {
            get { return _text; }
        }

        public OutputViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _container = container;
            Name = "Logger";
            Title = "Logger";
            ContentId = "Logger";
            IsVisible = false;
            
            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);
        }

        private void AddLog(ILoggerService logger)
        {
            _text = logger.Message + "\n" + _text;
            OnPropertyChanged(() => Text);
        }

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }
    }
}
