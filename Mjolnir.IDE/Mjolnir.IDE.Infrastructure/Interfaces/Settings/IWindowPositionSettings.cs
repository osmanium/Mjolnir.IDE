using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Settings
{
    public interface IWindowPositionSettings
    {
        double Height { get; }
        double Left { get; }
        WindowState State { get; }
        double Top { get; }
        double Width { get; }
    }
}