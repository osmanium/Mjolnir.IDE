using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mjolnir.IDE.Infrastructure.Interfaces
{
    public interface IApplicationDefinition
    {
        string ApplicationName { get; }
        ImageSource ApplicationIconSource { get; }

        void InitalizeIDE();

        void RegisterTypes();

        void LoadTheme();
        void LoadMenus();
        void LoadToolbar();
        void LoadSettings();


        void LoadModules();

        void OnIDELoaded();
        void onIDEClosing();
        void onIDEClosed();
    }
}
