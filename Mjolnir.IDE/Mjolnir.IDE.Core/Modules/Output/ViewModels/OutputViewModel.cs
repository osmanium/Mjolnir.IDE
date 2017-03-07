using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Output.Views;
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

namespace Mjolnir.IDE.Core.Modules.Output.ViewModels
{
    public class OutputViewModel : ToolViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IUnityContainer _container;
        private readonly OutputUserControl _view;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }


        private string _text;
        public string Text
        {
            get { return _text; }
        }


        public OutputViewModel(IUnityContainer container, AbstractWorkspace workspace)
            : base(container, null)
        {
            IsValidationEnabled = false;

            _container = container;
            Name = "Output";
            Title = "Output";
            ContentId = "Output";//TODO : Move to constants
            IsVisible = false;


            _view = new OutputUserControl(this);
            View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);
        }

        public void AddLog(IOutputService output)
        {
            _text = output.Message + "\n" + _text;
            OnPropertyChanged(() => Text);
        }
    }
}
