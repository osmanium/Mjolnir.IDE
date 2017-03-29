using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk.ViewModels;
using Mjolnir.IDE.Test.ProjectExplorer.Interfaces;
using Mjolnir.UI.Validation;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Test.ProjectExplorer.Views;
using Mjolnir.IDE.Test.ProjectExplorer.ProjectItems;

namespace Mjolnir.IDE.Test.ProjectExplorer.ViewModels
{
    public class ProjectExplorerViewModel : ToolViewModel
    {
        private readonly IProjectExplorerToolboxToolbarService _projectExplorerToolboxToolbarService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ProjectExplorerView _view;

        public ProjectTreeViewItem Root { get; set; }

        public ProjectExplorerViewModel(IProjectExplorerToolboxToolbarService toolbarService,
                                        IEventAggregator eventAggregator)
            : base(toolbarService)
        {
            _eventAggregator = eventAggregator;

            Name = "Project Explorer";
            Title = "Project Explorer";
            ContentId = "Project Explorer";//TODO : Move to constants
            IsVisible = false;


            _view = new ProjectExplorerView(this);
            View = _view;



            Root = new ProjectTreeViewItem(null, false) { DisplayName = "Project A" };

            var pluginsFolder = new FolderTreeViewItem(Root, false) { DisplayName = "Plugins Folder" };
            var pluginCs = new CsCodeTreeViewItem(pluginsFolder, false) { DisplayName = "Plugin.cs" };


            var executionFolder = new FolderTreeViewItem(Root, false) { DisplayName = "Execution Folder" };


            var businessUnits_Execution_Designer = new ExecutionDesignerTreeViewItem(executionFolder, false) { DisplayName = "BusinessUnits.xml" };
            var businessUnits_Execution = new ExecutionTreeViewItem(businessUnits_Execution_Designer, false) { DisplayName = "Project A Execution 1.cs" };


            var tempCs = new CsCodeTreeViewItem(Root, false) { DisplayName = "Temp.cs" };


            OnPropertyChanged(() => Root);
        }

        public override PaneLocation PreferredLocation => PaneLocation.Right;
    }
}
