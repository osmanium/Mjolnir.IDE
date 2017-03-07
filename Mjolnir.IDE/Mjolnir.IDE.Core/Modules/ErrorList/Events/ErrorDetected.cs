using Mjolnir.IDE.Infrastructure.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Modules.ErrorList.Events
{
    public class ErrorDetected : PubSubEvent<ErrorDetected>
    {
        public ErrorListItemType ItemType { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int? Line { get; set; }
        public int? Column { get; set; }
        public System.Action OnClick { get; set; }
    }
}
