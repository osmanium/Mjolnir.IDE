using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces
{
    public interface IApplicationDefinition
    {
        void LoadTheme();
        void LoadCommands();
        void LoadMenus();
        void LoadToolbar();
        void RegisterParts();
        void LoadSettings();
    }
}
