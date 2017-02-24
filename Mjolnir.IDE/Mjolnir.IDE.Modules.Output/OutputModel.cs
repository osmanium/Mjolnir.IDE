using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Output
{
    public class OutputModel : ValidatableBindableBase
    {
        private string _text;
        public string Text
        {
            get { return _text; }
        }

        public void AddLog(IOutputService output)
        {
            _text = output.Message + "\n" + _text;
            OnPropertyChanged(() => Text);
        }
    }
}
