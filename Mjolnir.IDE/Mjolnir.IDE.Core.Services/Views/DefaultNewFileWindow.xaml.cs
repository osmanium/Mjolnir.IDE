using Mjolnir.IDE.Core.Services.Attributes;
using System.Windows;

namespace Mjolnir.IDE.Core.Services.Views
{
    /// <summary>
    /// Interaction logic for DefaultNewFileWindow.xaml
    /// </summary>
    public partial class DefaultNewFileWindow : Window
    {
        public NewContentAttribute NewContent { get; private set; }

        public DefaultNewFileWindow()
        {
            InitializeComponent();
        }
    }
}
