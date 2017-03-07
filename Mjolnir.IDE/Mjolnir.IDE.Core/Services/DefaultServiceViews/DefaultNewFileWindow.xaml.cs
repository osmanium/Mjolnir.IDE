using Mjolnir.IDE.Infrastructure.Attributes;
using Mjolnir.IDE.Infrastructure.Interfaces.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mjolnir.IDE.Core.Services.DefaultServiceViews
{
    /// <summary>
    /// Interaction logic for DefaultNewFileWindow.xaml
    /// </summary>
    public partial class DefaultNewFileWindow : Window, INewFileWindowView
    {
        public NewContentAttribute NewContent { get; set; }

        public DefaultNewFileWindow()
        {
            InitializeComponent();
        }

        //TODO : Make here MVVM compatible
        private void listBoxItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.NewContent = (sender as ListBoxItem).DataContext as NewContentAttribute;
            this.DialogResult = true;
        }
        
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NewContent = this.listView.SelectedItem as NewContentAttribute;
            this.DialogResult = true;
        }
    }
}
