using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Converters;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Mjolnir.IDE.Shell.View
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : Window, IShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        public void LoadLayout()
        {
            //_eventAggregator.GetEvent<ThemeChangeEvent>().Subscribe(ThemeChanged);
            //_docContextMenu = new ContextMenu();
            //dockManager.DocumentContextMenu = _docContextMenu;
            //_docContextMenu.ContextMenuOpening += _docContextMenu_ContextMenuOpening;
            //_docContextMenu.Opened += _docContextMenu_Opened;
            //_itemSourceBinding = new MultiBinding();
            //_itemSourceBinding.Converter = new DocumentContextMenuMixingConverter();
            //var origModel = new Binding(".");
            //var docMenus = new Binding("Model.Menus");
            //_itemSourceBinding.Bindings.Add(origModel);
            //_itemSourceBinding.Bindings.Add(docMenus);
            //origModel.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //docMenus.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //_itemSourceBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //_docContextMenu.SetBinding(ContextMenu.ItemsSourceProperty, _itemSourceBinding);

            //Loaded += ShellViewMetro_Loaded;
        }

        private void ShellViewMetro_Loaded(object sender, RoutedEventArgs e)
        {
            //_eventAggregator.GetEvent<SplashCloseEvent>().Publish(new SplashCloseEvent());
        }

        #region IShell Members

        //public void LoadLayout()
        //{
        //    var layoutSerializer = new XmlLayoutSerializer(dockManager);
        //    layoutSerializer.LayoutSerializationCallback += (s, e) =>
        //    {
        //        var anchorable = e.Model as LayoutAnchorable;
        //        var document = e.Model as LayoutDocument;
        //        _workspace =
        //            _container.Resolve<AbstractWorkspace>();

        //        if (anchorable != null)
        //        {
        //            ToolViewModel model =
        //                _workspace.Tools.FirstOrDefault(
        //                    f => f.ContentId == e.Model.ContentId);
        //            if (model != null)
        //            {
        //                e.Content = model;
        //                model.IsVisible = anchorable.IsVisible;
        //                model.IsActive = anchorable.IsActive;
        //                model.IsSelected = anchorable.IsSelected;
        //            }
        //            else
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //        if (document != null)
        //        {
        //            var fileService =
        //                _container.Resolve<IOpenDocumentService>();
        //            ContentViewModel model =
        //                fileService.OpenFromID(e.Model.ContentId);
        //            if (model != null)
        //            {
        //                e.Content = model;
        //                model.IsActive = document.IsActive;
        //                model.IsSelected = document.IsSelected;
        //            }
        //            else
        //            {
        //                e.Cancel = true;
        //            }
        //        }
        //    };
        //    try
        //    {
        //        layoutSerializer.Deserialize(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "AvalonDock.Layout.config");
        //    }
        //    catch (Exception)
        //    {
        //        //TODO : Do not surpress exception
        //    }
        //}

        public void SaveLayout()
        {
            //var layoutSerializer = new XmlLayoutSerializer(dockManager);
            //layoutSerializer.Serialize(AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "AvalonDock.Layout.config");
        }

        #endregion

        #region Events

        //private void _docContextMenu_Opened(object sender, RoutedEventArgs e)
        //{
        //    RefreshMenuBinding();
        //}

        //private void _docContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        //{
        //    ///* When you right click a document - move the focus to that document, so that commands on the context menu
        //    // * which are based on the ActiveDocument work correctly. Example: Save.
        //    // */
        //    //LayoutDocumentItem doc = _docContextMenu.DataContext as LayoutDocumentItem;
        //    //if (doc != null)
        //    //{
        //    //    ContentViewModel model = doc.Model as ContentViewModel;
        //    //    if (model != null && model != dockManager.ActiveContent)
        //    //    {
        //    //        dockManager.ActiveContent = model;
        //    //    }
        //    //}
        //    //e.Handled = false;
        //}

        //private void RefreshMenuBinding()
        //{
        //    MultiBindingExpression b = BindingOperations.GetMultiBindingExpression(_docContextMenu,
        //                                                                           ContextMenu.ItemsSourceProperty);
        //    b.UpdateTarget();
        //}

        //private void ThemeChanged(ITheme theme)
        //{
        //    //HACK: Reset the context menu or else old menu status is retained and does not theme correctly
        //    //dockManager.DocumentContextMenu = null;
        //    //dockManager.DocumentContextMenu = _docContextMenu;
        //    _docContextMenu.Style = FindResource("MetroContextMenu") as Style;
        //    _docContextMenu.ItemContainerStyle = FindResource("MetroMenuStyle") as Style;
        //}

        //private void Window_Closing_1(object sender, CancelEventArgs e)
        //{
        //    var workspace = DataContext as IWorkspace;
        //    if (!workspace.Closing(e))
        //    {
        //        e.Cancel = true;
        //        return;
        //    }
        //    _eventAggregator.GetEvent<WindowClosingEvent>().Publish(this);
        //}

        //private void dockManager_ActiveContentChanged(object sender, EventArgs e)
        //{
        //    DockingManager manager = sender as DockingManager;
        //    ContentViewModel cvm = manager.ActiveContent as ContentViewModel;
        //    _eventAggregator.GetEvent<ActiveContentChangedEvent>().Publish(cvm);
        //    if (cvm != null) Logger.Log("Active document changed to " + cvm.Title, LogCategory.Info, LogPriority.None);
        //}

        //private void ContentControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    //HACK: Refresh the content control because in AutoHide mode this disappears. Needs to be fixed in AvalonDock.
        //    ContentControl c = sender as ContentControl;
        //    if (c != null)
        //    {
        //        var backup = c.Content;
        //        c.Content = null;
        //        c.Content = backup;
        //    }
        //}

        //#endregion

        //#region Property

        //private ILoggerService Logger
        //{
        //    get
        //    {
        //        if (_logger == null)
        //            _logger = _container.Resolve<ILoggerService>();

        //        return _logger;
        //    }
        //}

        //#endregion

    }
    #endregion
}