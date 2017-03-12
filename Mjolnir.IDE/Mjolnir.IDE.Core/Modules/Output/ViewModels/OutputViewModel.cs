using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Output.Views;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Enums;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
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
        private readonly IOutputToolboxToolbarService _outputToolbox;
        private readonly ICommandManager _commandManager;



        public const string DefaultOutputSource = "General";

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }


        private Dictionary<string, string> _outputSource;
        public Dictionary<string, string> OutputSource
        {
            get { return _outputSource; }
            set { SetProperty(ref _outputSource, value); }
        }

        private string _currentOutputContext;
        public string CurrentOutputContext
        {
            get
            {
                return _currentOutputContext == null ? DefaultOutputSource : _currentOutputContext;
            }
            set
            {
                SetProperty(ref _currentOutputContext, value);

                _aggregator.GetEvent<OutputSourceChangedEvent>().Publish(new OutputSourceChangedEvent() { EventSourceName = value });
            }
        }


        private string _text;
        public string Text
        {
            get { return _text; }
        }


        public OutputViewModel(IUnityContainer container,
                               AbstractWorkspace workspace,
                               IOutputToolboxToolbarService outputToolbox,
                               ICommandManager commandManager,
                               IEventAggregator aggregator)
            : base(container, outputToolbox)
        {
            IsValidationEnabled = false;

            _container = container;
            _outputToolbox = outputToolbox;
            _aggregator = aggregator;
            _commandManager = commandManager;

            _outputSource = new Dictionary<string, string>();
            _outputSource[DefaultOutputSource] = string.Empty;
            CurrentOutputContext = DefaultOutputSource;

            Name = "Output";
            Title = "Output";
            ContentId = "Output";//TODO : Move to constants
            IsVisible = false;


            _view = new OutputUserControl(this);
            View = _view;

            _aggregator.GetEvent<LogEvent>().Subscribe(AddLog);
            _aggregator.GetEvent<OutputSourceAddedEvent>().Subscribe(OutputSourceAddedEvent);
            _aggregator.GetEvent<OutputSourceRemovedEvent>().Subscribe(OutputSourceRemovedEvent);
            _aggregator.GetEvent<OutputSourceChangedEvent>().Subscribe(OutputSourceChangedEvent);
        }


        public void OutputSourceChangedEvent(OutputSourceChangedEvent e)
        {

            if (!string.IsNullOrWhiteSpace(e.EventSourceName))
            {
                _text = _outputSource[e.EventSourceName];
            }
            else
            {
                _text = _outputSource[DefaultOutputSource];
            }

            OnPropertyChanged(() => Text);
        }

        public void OutputSourceAddedEvent(OutputSourceAddedEvent e)
        {
            _outputSource[e.OutputSourceName] = string.Empty;

            _outputToolbox.Get("OutputSource").Get("_Output_Source")
                .Add(new MenuItemViewModel(e.OutputSourceName, e.OutputSourceName, 1,
                null, _commandManager.GetCommand("DONOTHING"), null, false, false, null, false, false));


            OnPropertyChanged(() => OutputSource);
        }

        public void OutputSourceRemovedEvent(OutputSourceRemovedEvent e)
        {
            _outputSource[e.OutputSourceName] = string.Empty;
            OnPropertyChanged(() => OutputSource);
        }



        public void AddLog(IOutputService output)
        {
            if (output.OutputSource != null && CurrentOutputContext == output.OutputSource)
            {
                _text = output.Message + "\n" + _text;
                _outputSource[output.OutputSource] = _text;
                OnPropertyChanged(() => Text);
            }
            else if (output.OutputSource != null && CurrentOutputContext != output.OutputSource)
            {
                //Apply to non selected output source
                _outputSource[output.OutputSource] += output.Message + "\n" + _text;
            }
            else if (output.OutputSource == null)
            {
                //Append to default
                _outputSource[DefaultOutputSource] += output.Message + "\n" + _text;
                

                if (CurrentOutputContext == DefaultOutputSource)
                    OnPropertyChanged(() => Text);
            }
        }

        public void ClearLog()
        {
            _outputSource[CurrentOutputContext] = string.Empty;
            _text = string.Empty;
            OnPropertyChanged(() => Text);
        }
    }
}
