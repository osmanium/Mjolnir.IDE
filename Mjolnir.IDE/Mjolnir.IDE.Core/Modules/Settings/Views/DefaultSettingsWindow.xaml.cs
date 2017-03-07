using System.Windows;

namespace Mjolnir.IDE.Core.Modules.Settings.Views
{
    /// <summary>
    /// Interaction logic for DefaultSettingsWindow.xaml
    /// </summary>
    public partial class DefaultSettingsWindow : Window
    {
        public DefaultSettingsWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
