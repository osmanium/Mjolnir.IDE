using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk
{
    public class ToolboxItem
    {
        public Type DocumentType { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Uri IconSource { get; set; }
        public Type ItemType { get; set; }
    }
}
