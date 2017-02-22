using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Test
{
    public class CoreModule : IApplicationDefinition
    {
        private IUnityContainer _container;

        public CoreModule(IUnityContainer container)
        {

            _container = container;
        }

        public void LoadMenus()
        {
            var menuService = _container.Resolve<IMenuService>();
            var manager = _container.Resolve<ICommandManager>();
            var settingsManager = _container.Resolve<ISettingsManager>();
            var themeSettings = _container.Resolve<IThemeSettings>();
            var recentFiles = _container.Resolve<IRecentViewSettings>();
            //IWorkspace workspace = _container.Resolve<AbstractWorkspace>();
            //ToolViewModel logger = workspace.Tools.First(f => f.ContentId == "Logger");

            menuService.Add(new MenuItemViewModel("_File", 1));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_New", 3,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/NewRequest_8796.png")),
                                       manager.GetCommand("NEW"),
                                       new KeyGesture(Key.N, ModifierKeys.Control, "Ctrl + N"))));

            menuService.Get("_File").Add(
                (new MenuItemViewModel("_Open", 4,
                                       new BitmapImage(
                                           new Uri(
                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/OpenFileDialog_692.png")),
                                       manager.GetCommand("OPEN"),
                                       new KeyGesture(Key.O, ModifierKeys.Control, "Ctrl + O"))));
            menuService.Get("_File").Add(new MenuItemViewModel("_Save", 5,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
                                                               manager.GetCommand("SAVE"),
                                                               new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl + S")));
            //menuService.Get("_File").Add(new SaveAsMenuItemViewModel("Save As..", 6,
            //                                       new BitmapImage(
            //                                           new Uri(
            //                                               @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Save_6530.png")),
            //                                       manager.GetCommand("SAVEAS"), null, false, false, _container));

            menuService.Get("_File").Add(new MenuItemViewModel("Close", 8, null, manager.GetCommand("CLOSE"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Control, "Ctrl + F4")));

            menuService.Get("_File").Add(recentFiles.RecentMenu);

            menuService.Get("_File").Add(new MenuItemViewModel("E_xit", 101, null, manager.GetCommand("EXIT"),
                                                               new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt + F4")));


            menuService.Add(new MenuItemViewModel("_Edit", 2));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Undo", 1,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Undo_16x.png")),
                                                               ApplicationCommands.Undo));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Redo", 2,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Redo_16x.png")),
                                                               ApplicationCommands.Redo));
            menuService.Get("_Edit").Add(MenuItemViewModel.Separator(15));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Cut", 20,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Cut_6523.png")),
                                                               ApplicationCommands.Cut));
            menuService.Get("_Edit").Add(new MenuItemViewModel("Copy", 21,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Copy_6524.png")),
                                                               ApplicationCommands.Copy));
            menuService.Get("_Edit").Add(new MenuItemViewModel("_Paste", 22,
                                                               new BitmapImage(
                                                                   new Uri(
                                                                       @"pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Paste_6520.png")),
                                                               ApplicationCommands.Paste));

            menuService.Add(new MenuItemViewModel("_View", 3));

//            if (logger != null)
//                menuService.Get("_View").Add(new MenuItemViewModel("_Logger", 1,
//                                                                   new BitmapImage(
//                                                                       new Uri(
//                                                                           @"pack://application:,,,/Mjolnir.IDE.Test
//;component/Icons/Undo_16x.png")),
//                                                                   manager.GetCommand("LOGSHOW"))
//                { IsCheckable = true, IsChecked = logger.IsVisible });

            menuService.Get("_View").Add(new MenuItemViewModel("Themes", 1));

            //Set the checkmark of the theme menu's based on which is currently selected
            menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Dark", 1, null,
                                                                             manager.GetCommand("THEMECHANGE"))
            {
                IsCheckable = true,
                IsChecked = (themeSettings.SelectedTheme == "Dark"),
                CommandParameter = "Dark"
            });
            menuService.Get("_View").Get("Themes").Add(new MenuItemViewModel("Light", 2, null,
                                                                             manager.GetCommand("THEMECHANGE"))
            {
                IsCheckable = true,
                IsChecked = (themeSettings.SelectedTheme == "Light"),
                CommandParameter = "Light"
            });

            menuService.Add(new MenuItemViewModel("_Tools", 4));
            menuService.Get("_Tools").Add(new MenuItemViewModel("Settings", 1, null, settingsManager.SettingsCommand));

            menuService.Add(new MenuItemViewModel("_Help", 4));
        }

        public void LoadSettings()
        {
        }

        public void LoadTheme()
        {
        }

        public void LoadToolbar()
        {
            //var toolbarService = _container.Resolve<IToolbarService>();
            var menuService = _container.Resolve<IMenuService>();
            //var manager = _container.Resolve<ICommandManager>();

            //toolbarService.Add(new ToolbarViewModel("Standard", 1) { Band = 1, BandIndex = 1 });
            //toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_New"));
            //toolbarService.Get("Standard").Add(menuService.Get("_File").Get("_Open"));

            //toolbarService.Add(new ToolbarViewModel("Edit", 1) { Band = 1, BandIndex = 2 });
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Undo"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Redo"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Cut"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("Copy"));
            //toolbarService.Get("Edit").Add(menuService.Get("_Edit").Get("_Paste"));

            //toolbarService.Add(new ToolbarViewModel("Debug", 1) { Band = 1, BandIndex = 3 });
            //toolbarService.Get("Debug").Add(new MenuItemViewModel("Debug", 1, new BitmapImage(new Uri(@"pack://application:,,,/WideMD.Core;component/Icons/Play.png"))));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with Chrome", 1, new BitmapImage(new Uri(@"pack://application:,,,/WideMD.Core;component/Icons/Play.png")), manager.GetCommand("OPEN")));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with FireFox", 2, new BitmapImage(new Uri(@"pack://application:,,,/WideMD.Core;component/Icons/Play.png")), manager.GetCommand("OPEN")));
            //toolbarService.Get("Debug").Get("Debug").Add(new MenuItemViewModel("Debug with Explorer", 3, new BitmapImage(new Uri(@"pack://application:,,,/WideMD.Core;component/Icons/Play.png")), manager.GetCommand("OPEN")));

            //menuService.Get("_Tools").Add(toolbarService.RightClickMenu);

            //Initiate the position settings changes for toolbar
            //_container.Resolve<IToolbarPositionSettings>();
        }

        public void RegisterParts()
        {
        }

        public void LoadCommands()
        {
        }
    }
}
