using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Core.Services
{
    /// <summary>
    /// The main theme manager used in Wide
    /// </summary>
    public sealed class ThemeManager : IThemeManager
    {
        /// <summary>
        /// Dictionary of different themes
        /// </summary>
        private static readonly Dictionary<string, ITheme> ThemeDictionary = new Dictionary<string, ITheme>();

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// The theme manager constructor
        /// </summary>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public ThemeManager(IEventAggregator eventAggregator, ILoggerService logger)
        {
            Themes = new ObservableCollection<ITheme>();
            _eventAggregator = eventAggregator;
            _logger = logger;
        }

        /// <summary>
        /// The current theme set in the theme manager
        /// </summary>
        public ITheme CurrentTheme { get; internal set; }

        #region IThemeManager Members

        /// <summary>
        /// The collection of themes
        /// </summary>
        public ObservableCollection<ITheme> Themes { get; internal set; }

        /// <summary>
        /// Set the current theme
        /// </summary>
        /// <param name="name">The name of the theme</param>
        /// <returns>true if the new theme is set, false otherwise</returns>
        public bool SetCurrent(string name)
        {
            if (ThemeDictionary.ContainsKey(name))
            {
                ITheme newTheme = ThemeDictionary[name];
                CurrentTheme = newTheme;

                ResourceDictionary theme = Application.Current.MainWindow.Resources.MergedDictionaries[0];
                ResourceDictionary appTheme = Application.Current.Resources.MergedDictionaries.Count > 0
                                                  ? Application.Current.Resources.MergedDictionaries[0]
                                                  : null;
                theme.BeginInit();
                theme.MergedDictionaries.Clear();
                if (appTheme != null)
                {
                    appTheme.BeginInit();
                    appTheme.MergedDictionaries.Clear();
                }
                else
                {
                    appTheme = new ResourceDictionary();
                    appTheme.BeginInit();
                    Application.Current.Resources.MergedDictionaries.Add(appTheme);
                }
                foreach (Uri uri in newTheme.UriList)
                {
                    try
                    {
                        ResourceDictionary newDict = new ResourceDictionary { Source = uri };
                        /*AvalonDock and menu style needs to move to the application
                         * 1. AvalonDock needs global styles as floatable windows can be created
                         * 2. Menu's need global style as context menu can be created
                        */
                        if (uri.ToString().Contains("AvalonDock") ||
                            uri.ToString().Contains("Mjolnir.IDE.Shell;component/Styles/VS2013/Menu.xaml"))
                        {
                            appTheme.MergedDictionaries.Add(newDict);
                        }
                        else
                        {
                            theme.MergedDictionaries.Add(newDict);
                        }

                    }
                    catch (Exception ex)
                    {
                        //TODO : Log error
                        Debugger.Break();
                    }
                }
                appTheme.EndInit();
                theme.EndInit();
                _logger.Log("Theme set to " + name, LogCategory.Info, LogPriority.None);
                _eventAggregator.GetEvent<ThemeChangeEvent>().Publish(newTheme);
            }
            return false;
        }

        /// <summary>
        /// Adds a theme to the theme manager
        /// </summary>
        /// <param name="theme">The theme to add</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public bool AddTheme(ITheme theme)
        {
            if (!ThemeDictionary.ContainsKey(theme.Name))
            {
                ThemeDictionary.Add(theme.Name, theme);
                Themes.Add(theme);
                return true;
            }
            return false;
        }

        #endregion
    }
}
