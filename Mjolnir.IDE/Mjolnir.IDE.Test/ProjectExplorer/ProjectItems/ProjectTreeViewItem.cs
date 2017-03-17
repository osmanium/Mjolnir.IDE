using Mjolnir.IDE.Test.ProjectExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.ProjectExplorer.ProjectItems
{
    public class ProjectTreeViewItem : TreeItemViewModel
    {
        public ProjectTreeViewItem(TreeItemViewModel parent, bool lazyLoadChildren)
            : base(parent, lazyLoadChildren)
        {
        }
    }
}
