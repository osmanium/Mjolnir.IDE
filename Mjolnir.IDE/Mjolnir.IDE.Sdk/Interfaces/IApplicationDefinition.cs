using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Sdk.Interfaces
{
    public interface IApplicationDefinition
    {
        string ApplicationName { get; set; }
        ImageSource ApplicationIconSource { get; set; }


        void InitalizeIDE();

        void RegisterTypes();

        void LoadTheme();
        void LoadMenus();
        void LoadToolbar();
        void LoadSettings();
        void LoadCommands();

        void LoadModules();

        void OnIDELoaded();
        bool onIDEClosing();
        void onIDEClosed();
    }
}
