using Mjolnir.IDE.Test.ProjectExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mjolnir.IDE.Test.ProjectExplorer.ProjectItems
{
    public class ExecutionTreeViewItem : TreeItemViewModel
    {
        public ExecutionTreeViewItem(TreeItemViewModel parent, bool lazyLoadChildren)
            : base(parent, lazyLoadChildren)
        {
        }
    }
}
