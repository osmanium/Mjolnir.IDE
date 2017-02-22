﻿using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Controls;

namespace Mjolnir.IDE.Infrastructure.Converters
{
    public class DocumentContextMenuMixingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            AbstractMenuItem root = new MenuItemViewModel("$CROOT$", 1);
            LayoutDocumentItem doc = values[0] as LayoutDocumentItem;
            int i = 1;
            IReadOnlyCollection<AbstractMenuItem> menus = values[1] as IReadOnlyCollection<AbstractMenuItem>;
            ContextMenu cm;
            if (doc != null)
            {
                try
                {
                    //TODO make it 2013
                    cm = Application.Current.FindResource("AvalonDock_ThemeVS2012_DocumentContextMenu") as ContextMenu;
                    if (cm != null)
                    {
                        foreach (MenuItem mi in cm.Items)
                        {
                            root.Add(FromMenuItem(mi, doc, i++));
                        }
                    }
                }
                catch
                {
                }

                if (menus != null)
                {
                    foreach (AbstractMenuItem abstractMenuItem in menus)
                    {
                        root.Add(abstractMenuItem);
                    }
                }
            }
            return root.Children;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
                                    System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private AbstractMenuItem FromMenuItem(MenuItem item, LayoutDocumentItem doc, int priority)
        {
            bool hideDisabled = false;
            if (item != null)
            {
                ICommand cmd = null;
                if (doc != null)
                {
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_Close)
                    {
                        cmd = doc.CloseCommand;
                    }
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_CloseAllButThis)
                    {
                        cmd = doc.CloseAllButThisCommand;
                    }
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_Float)
                    {
                        cmd = doc.FloatCommand;
                        hideDisabled = true;
                    }
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_DockAsDocument)
                    {
                        cmd = doc.DockAsDocumentCommand;
                        hideDisabled = true;
                    }
                    if (item.Header.ToString() ==
                        Xceed.Wpf.AvalonDock.Properties.Resources.Document_NewHorizontalTabGroup)
                    {
                        cmd = doc.NewHorizontalTabGroupCommand;
                        hideDisabled = true;
                    }
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_NewVerticalTabGroup)
                    {
                        cmd = doc.NewVerticalTabGroupCommand;
                        hideDisabled = true;
                    }
                    if (item.Header.ToString() == Xceed.Wpf.AvalonDock.Properties.Resources.Document_MoveToNextTabGroup)
                    {
                        cmd = doc.MoveToNextTabGroupCommand;
                        hideDisabled = true;
                    }
                    if (item.Header.ToString() ==
                        Xceed.Wpf.AvalonDock.Properties.Resources.Document_MoveToPreviousTabGroup)
                    {
                        cmd = doc.MoveToPreviousTabGroupCommand;
                        hideDisabled = true;
                    }
                }

                MenuItemViewModel model = new MenuItemViewModel(item.Header.ToString(), priority,
                                                                item.Icon != null ? (item.Icon as Image).Source : null,
                                                                cmd, null, false, hideDisabled);
                return model;
            }
            return null;
        }
    }
}
