using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Services
{
    public interface IStatusbarService
    {
        bool Animation(Image image);
        bool Clear();
        bool FreezeOutput();
        bool IsFrozen { get; }
        string Text { get; set; }
        Brush Foreground { get; set; }
        Brush Background { get; set; }
        bool? InsertMode { get; set; }
        int? LineNumber { get; set; }
        int? CharPosition { get; set; }
        int? ColPosition { get; set; }
        bool Progress(bool On, uint current, uint total);
    }
}
