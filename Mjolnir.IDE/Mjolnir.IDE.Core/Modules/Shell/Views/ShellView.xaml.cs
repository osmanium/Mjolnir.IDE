using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;
using Mjolnir.IDE.Sdk;
using Mjolnir.IDE.Sdk.Converters;
using Mjolnir.IDE.Sdk.Enums;
using Mjolnir.IDE.Sdk.Events;
using Mjolnir.IDE.Sdk.Interfaces;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.Interfaces.Views;
using Mjolnir.IDE.Sdk.ViewModels;
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

namespace Mjolnir.IDE.Core.Modules.Shell.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow, IShellView
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public ShellView(IUnityContainer container, IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public void LoadLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.LayoutSerializationCallback += (s, e) =>
            {
                var anchorable = e.Model as LayoutAnchorable;
                var document = e.Model as LayoutDocument;
                IWorkspace workspace = _container.Resolve<DefaultWorkspace>();

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
                outputService.LogOutput(new LogOutputItem(ex.Message, OutputCategory.Exception, OutputPriority.High));
            }
        }

        public void SaveLayout()
        {
            var layoutSerializer = new XmlLayoutSerializer(dockManager);
            layoutSerializer.Serialize(AppDomain.CurrentDomain.BaseDirectory + "\\AvalonDock.Layout.config");
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _eventAggregator.GetEvent<IDEClosedEvent>().Publish();
        }
        protected override void OnClosed(EventArgs e)
        {
            _eventAggregator.GetEvent<IDEClosedEvent>().Publish();

            base.OnClosed(e);
        }
    }
}