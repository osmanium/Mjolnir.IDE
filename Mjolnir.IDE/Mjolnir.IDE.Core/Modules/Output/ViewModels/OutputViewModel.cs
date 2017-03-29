using Microsoft.Practices.Unity;
using Mjolnir.IDE.Core.Modules.Output.Views;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Interfaces.ViewModels;
using Mjolnir.IDE.Sdk.ViewModels;
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
        #region Fields
        private readonly IEventAggregator _aggregator;
        private readonly OutputUserControl _view;
        private readonly IOutputToolboxToolbarService _outputToolbox;
        private readonly ICommandManager _commandManager;
        #endregion

        #region Constants
        public const string DefaultOutputSource = "General";
        #endregion

        #region Properties
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

        #endregion

        #region Constructors
        public OutputViewModel(DefaultWorkspace workspace,
                               IOutputToolboxToolbarService outputToolbox,
                               ICommandManager commandManager,
                               IEventAggregator aggregator)
            : base(outputToolbox)
        {
            IsValidationEnabled = false;

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

        #endregion
        
        #region Public Methods
        public void OutputSourceRefresh()
        {
            OnPropertyChanged(() => CurrentOutputContext);
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

        public void AddOutputSource()
        {
            throw new NotImplementedException();
        }

        public void RemoveOutputSource()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        private void OutputSourceChangedEvent(OutputSourceChangedEvent e)
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

        private void OutputSourceAddedEvent(OutputSourceAddedEvent e)
        {
            _outputSource[e.OutputSourceName] = string.Empty;

            _outputToolbox.Get("OutputSource").Get("_Output_Source")
                .Add(new MenuItemViewModel(e.OutputSourceName, e.OutputSourceName, 1,
                null, _commandManager.GetCommand("DONOTHING"), null, false, false, null, false, false));


            OnPropertyChanged(() => OutputSource);
        }

        private void OutputSourceRemovedEvent(OutputSourceRemovedEvent e)
        {
            _outputSource[e.OutputSourceName] = string.Empty;
            OnPropertyChanged(() => OutputSource);
        }
        #endregion
    }
}
