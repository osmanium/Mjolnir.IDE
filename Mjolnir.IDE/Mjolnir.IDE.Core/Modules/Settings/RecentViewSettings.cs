using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mjolnir.IDE.Core.Modules.Settings
{
    public class RecentViewSettings : AbstractSettings, IRecentViewSettings
    {
        private AbstractMenuItem recentMenu;
        private List<string> menuGuids;
        private DelegateCommand<string> recentOpen;
        //private IOpenDocumentService fileService;
        private IUnityContainer _container;

        public RecentViewSettings(IUnityContainer container)
        {
            recentMenu = new MenuItemViewModel("Recentl_y opened..", "Recentl_y opened..", 100);
            menuGuids = new List<string>();
            recentOpen = new DelegateCommand<string>(ExecuteMethod);
            this._container = container;
        }

        [UserScopedSetting()]
        public List<RecentViewItem> ActualRecentItems
        {
            get
            {
                if ((List<RecentViewItem>)this["ActualRecentItems"] == null)
                    this["ActualRecentItems"] = new List<RecentViewItem>((int)TotalItems);
                return (List<RecentViewItem>)this["ActualRecentItems"];
            }
            set { this["ActualRecentItems"] = value; }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("10")]
        public uint TotalItems
        {
            get { return (uint)this["TotalItems"]; }
            set
            {
                this["TotalItems"] = value;
                ActualRecentItems.Capacity = (int)value;
                menuGuids.Capacity = (int)value;
            }
        }

        public void Update(ContentViewModel viewModel)
        {
            RecentViewItem item = new RecentViewItem();
            item.ContentID = viewModel.ContentId;
            item.DisplayValue = viewModel.Model.Location.ToString();

            if (ActualRecentItems.Contains(item))
            {
                ActualRecentItems.Remove(item);
            }
            ActualRecentItems.Add(item);
            this.Save();
            RecentMenu.Refresh();
        }

        private void ExecuteMethod(string s)
        {
            //if (fileService == null)
            //{
            //    fileService = _container.Resolve<IOpenDocumentService>();
            //}
            //fileService.OpenFromID(s, true);
        }

        [XmlIgnore]
        public AbstractMenuItem RecentMenu
        {
            get
            {
                int i = RecentItems.Count;
                foreach (string guid in menuGuids)
                {
                    recentMenu.Remove(guid);
                    i--;
                }

                menuGuids.Clear();

                for (i = RecentItems.Count; i > 0; i--)
                {
                    int priority = RecentItems.Count - i + 1;
                    string number = "_" + priority.ToString() + " " + RecentItems[i - 1].DisplayValue;
                    menuGuids.Add(recentMenu.Add(new MenuItemViewModel(number, number, priority, null, recentOpen, null)
                    { CommandParameter = RecentItems[i - 1].ContentID }));
                }
                return recentMenu;
            }
        }

        [XmlIgnore]
        public IReadOnlyList<IRecentViewItem> RecentItems
        {
            get { return this.ActualRecentItems.AsReadOnly(); }
        }
    }
}