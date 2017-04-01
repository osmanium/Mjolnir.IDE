using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mjolnir.IDE.Core.Modules.Shell.ViewModels
{
    public class ShellViewModel : ValidatableBindableBase
    {
        private MjolnirApp _app;
        public MjolnirApp App
        {
            get { return _app; }
        }

        private DefaultWorkspace _abstractWorkspace;
        public DefaultWorkspace Workspace
        {
            get { return _abstractWorkspace; }
            set { SetProperty(ref _abstractWorkspace, value); }
        }

        public ShellViewModel(MjolnirApp app)
        {
            IsValidationEnabled = false;
            _app = app;
        }
    }
}
