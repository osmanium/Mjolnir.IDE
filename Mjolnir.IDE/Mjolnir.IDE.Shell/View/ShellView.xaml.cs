using MahApps.Metro.Controls;
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
    public partial class ShellView : MetroWindow, IShellView
    {
        private IUnityContainer _container;

        public ShellView(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
        }

        public void LoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                var anchorable = e.Model as LayoutAnchorable;
                var document = e.Model as LayoutDocument;
                IWorkspace workspace = _container.Resolve<AbstractWorkspace>();

                if (anchorable != null)
                {
                    ToolViewModel model = workspace.Tools.FirstOrDefault(f => f.ContentId == e.Model.ContentId);
                    if (model != null)
                    {
                        e.Content = model;
                        model.IsVisible = anchorable.IsVisible;
                        model.IsActive = anchorable.IsActive;
                        model.IsSelected = anchorable.IsSelected;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                if (document != null)
                {
                    var fileService =
                        _container.Resolve<IOpenDocumentService>();
                    ContentViewModel model =
                        fileService.OpenFromID(e.Model.ContentId);
                    if (model != null)
                    {
                        e.Content = model;
                        model.IsActive = document.IsActive;
                        model.IsSelected = document.IsSelected;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            };
            try
            {
                layoutSerializer.Deserialize(AppDomain.CurrentDomain.BaseDirectory + "\\AvalonDock.Layout.config");
            }
            catch (Exception ex)
            {
                var outputService = _container.Resolve<IOutputService>();
                outputService.LogOutput(ex.Message, OutputCategory.Exception, OutputPriority.High);
            }
        }

        public void SaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(AppDomain.CurrentDomain.BaseDirectory + "\\AvalonDock.Layout.config");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            var applicationDefinition = _container.Resolve<IApplicationDefinition>();
            if (applicationDefinition != null)
            {
                if (!applicationDefinition.onIDEClosing())
                {
                    var shell = _container.Resolve<IShellView>();
                    shell.SaveLayout();

                    applicationDefinition.onIDEClosing();
                }
                else
                {
                    e.Cancel = true;
                    base.OnClosing(e);
                }
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            var applicationDefinition = _container.Resolve<IApplicationDefinition>();
            if (applicationDefinition != null)
            {
                applicationDefinition.onIDEClosed();
            }

            base.OnClosed(e);
        }
    }
}