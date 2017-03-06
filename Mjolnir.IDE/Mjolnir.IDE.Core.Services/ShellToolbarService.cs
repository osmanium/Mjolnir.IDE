using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Xceed.Wpf.AvalonDock.Converters;

namespace Mjolnir.IDE.Core.Services
{
    /// <summary>
    /// The Wide tool bar service
    /// </summary>
    public sealed class ShellToolbarService : AbstractToolbar, IShellToolbarService
    {
        private static BoolToVisibilityConverter btv = new BoolToVisibilityConverter();
        private AbstractMenuItem menuItem;
        private ToolBarTray tray;

        public ShellToolbarService() : base("$MAIN$", string.Empty, 0)
        {
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        public override string Add(AbstractCommandable item)
        {
            AbstractToolbar tb = item as AbstractToolbar;
            if (tb != null)
            {
                tb.IsCheckable = true;
                tb.IsChecked = true;
            }
            return base.Add(item);
        }

        /// <summary>
        /// The toolbar tray which will be used in the application
        /// </summary>
        public ToolBarTray ToolBarTray
        {
            get
            {
                if (tray == null)
                {
                    tray = new ToolBarTray();
                    tray.ContextMenu = new ContextMenu();
                    tray.ContextMenu.ItemsSource = _children;
                    IAddChild child = tray;
                    foreach (var node in this.Children)
                    {
                        var value = node as AbstractToolbar;
                        if (value != null)
                        {
                            var tb = new ToolBar();
                            var t =
                                Application.Current.MainWindow.FindResource("toolBarItemTemplateSelector") as
                                DataTemplateSelector;
                            tb.SetValue(ItemsControl.ItemTemplateSelectorProperty, t);

                            //Set the necessary bindings
                            var bandBinding = new Binding("Band");
                            var bandIndexBinding = new Binding("BandIndex");
                            var visibilityBinding = new Binding("IsChecked")
                            {
                                Converter = btv
                            };

                            bandBinding.Source = value;
                            bandIndexBinding.Source = value;
                            visibilityBinding.Source = value;

                            bandBinding.Mode = BindingMode.TwoWay;
                            bandIndexBinding.Mode = BindingMode.TwoWay;

                            tb.SetBinding(ToolBar.BandProperty, bandBinding);
                            tb.SetBinding(ToolBar.BandIndexProperty, bandIndexBinding);
                            tb.SetBinding(ToolBar.VisibilityProperty, visibilityBinding);

                            tb.ItemsSource = value.Children;
                            child.AddChild(tb);
                        }
                    }
                    tray.ContextMenu.ItemContainerStyle =
                        Application.Current.MainWindow.FindResource("ToolbarContextMenu") as Style;
                }
                return tray;
            }
        }

        public AbstractMenuItem RightClickMenu
        {
            get
            {
                if (tray == null)
                {
                    tray = this.ToolBarTray;
                }
                if (menuItem == null)
                {
                    menuItem = new MenuItemViewModel("_Toolbars", "_Toolbars", 100);
                    foreach (var value in tray.ContextMenu.ItemsSource)
                    {
                        var menu = value as AbstractMenuItem;
                        menuItem.Add(menu);
                    }
                }
                return menuItem;
            }
        }
    }
}