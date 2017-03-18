using Mjolnir.IDE.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.Toolbox.ViewModels
{
    public class ToolboxItemViewModel
    {
        private readonly ToolboxItem _model;

        public ToolboxItem Model
        {
            get { return _model; }
        }

        public string Name
        {
            get { return _model.Name; }
        }

        public virtual string Category
        {
            get { return _model.Category; }
        }

        public virtual Uri IconSource
        {
            get { return _model.IconSource; }
        }

        public ToolboxItemViewModel(ToolboxItem model)
        {
            _model = model;
        }
    }
}