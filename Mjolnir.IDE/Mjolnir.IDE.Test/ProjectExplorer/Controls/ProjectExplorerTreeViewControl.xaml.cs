using Mjolnir.IDE.Test.ProjectExplorer.ViewModels;
using Mjolnir.IDE.Test.ProjectExplorer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mjolnir.IDE.Test.ProjectExplorer.Controls
{
    /// <summary>
    /// Interaction logic for ProjectExplorerTreeViewControl.xaml
    /// </summary>
    public partial class ProjectExplorerTreeViewControl : UserControl
    {

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        public static readonly DependencyProperty IsInEditModeProperty =
            DependencyProperty.Register("IsInEditMode", typeof(bool), typeof(ProjectExplorerTreeViewControl), new PropertyMetadata(false));



        public ObservableCollection<TreeItemViewModel> ExplorerItemsSource
        {
            get { return (ObservableCollection<TreeItemViewModel>)GetValue(ExplorerItemsSourceProperty); }
            set { SetValue(ExplorerItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ExplorerItemsSourceProperty =
            DependencyProperty.Register("ExplorerItemsSource", typeof(ObservableCollection<TreeItemViewModel>), typeof(ProjectExplorerTreeViewControl), new PropertyMetadata(null));
        

        public ProjectExplorerTreeViewControl()
        {
            InitializeComponent();
        }


        // text in a text box before editing - to enable cancelling changes
        string oldText;



        // if a text box has just become visible, we give it the keyboard input focus and select contents
        private void editableTextBoxHeader_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.IsVisible)
            {
                tb.Focus();

                if (!String.IsNullOrWhiteSpace(tb.Text))
                {
                    if (tb.Text.Contains('.'))
                    {
                        var lastIndex = tb.Text.LastIndexOf('.');
                        tb.Select(0, lastIndex);
                    }
                    else
                        tb.SelectAll();
                }


                oldText = tb.Text;      // back up - for possible cancelling
            }
        }

        // stop editing on Enter or Escape (then with cancel)
        private void editableTextBoxHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                IsInEditMode = false;
            if (e.Key == Key.Escape)
            {
                var tb = sender as TextBox;
                tb.Text = oldText;
                IsInEditMode = false;
            }
        }

        // stop editing on lost focus
        private void editableTextBoxHeader_LostFocus(object sender, RoutedEventArgs e)
        {
            IsInEditMode = false;
        }

        // it might happen, that the user pressed F2 while a non-editable item was selected
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IsInEditMode = false;
        }

        // we (possibly) switch to edit mode when the user presses F2
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
                IsInEditMode = true;
        }

        // the user has clicked a header - proceed with editing if it was selected
        private void textBlockHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FindTreeItem(e.OriginalSource as DependencyObject).IsSelected)
            {
                IsInEditMode = true;
                e.Handled = true;       // otherwise the newly activated control will immediately loose focus
            }
        }

        // searches for the corresponding TreeViewItem,
        // based on http://stackoverflow.com/questions/592373/select-treeview-node-on-right-click-before-displaying-contextmenu
        static TreeViewItem FindTreeItem(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }
    }
}
