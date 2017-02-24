using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Enums;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Modules.Output.Views;
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
        private readonly OutputModel _model;
        private readonly OutputUserControl _view;


        

        public OutputViewModel(IUnityContainer container, AbstractWorkspace workspace)
        {
            _container = container;
            Name = "Output";
            Title = "Output";
            ContentId = "Output";
            IsVisible = false;

            _model = new OutputModel();
            Model = _model;

            _view = new OutputUserControl(_model);
            View = _view;

            _aggregator = _container.Resolve<IEventAggregator>();
            _aggregator.GetEvent<LogEvent>().Subscribe(AddOutput);
        }

        private void AddOutput(IOutputService output)
        {
            _model.AddLog(output);
        }

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }
    }
}
