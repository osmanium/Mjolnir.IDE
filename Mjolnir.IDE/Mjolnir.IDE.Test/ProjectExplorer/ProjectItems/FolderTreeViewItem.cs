using Mjolnir.IDE.Test.ProjectExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mjolnir.IDE.Test.ProjectExplorer.ProjectItems
{
    public class FolderTreeViewItem : TreeItemViewModel
    {
        public FolderTreeViewItem(TreeItemViewModel parent, bool lazyLoadChildren)
            : base(parent, lazyLoadChildren)
        {

        }
    }
}
