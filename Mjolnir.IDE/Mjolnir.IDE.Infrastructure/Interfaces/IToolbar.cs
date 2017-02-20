using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces
{
    public interface IToolbar
    {
        /// <summary>
        /// Gets the header of the toolbar.
        /// </summary>
        /// <value>The header.</value>
        string Header { get; }

        /// <summary>
        /// Gets the band number for the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The band.</value>
        int Band { get; }

        /// <summary>
        /// Gets the band index of the toolbar in the toolbar tray.
        /// </summary>
        /// <value>The index of the band.</value>
        int BandIndex { get; }

        /// <summary>
        /// Gets a value indicating whether this toolbar is visible.
        /// </summary>
        /// <value><c>true</c> if this toolbar is visible; otherwise, <c>false</c>.</value>
        bool IsChecked { get; }
    }
}