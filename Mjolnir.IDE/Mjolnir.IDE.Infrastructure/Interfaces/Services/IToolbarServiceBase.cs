using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Interface IToolbarService - the application's toolbar tray is returned by this service
    /// </summary>
    public interface IToolbarServiceBase
    {
        /// <summary>
        /// Gets the tool bar tray of the application.
        /// </summary>
        /// <value>The tool bar tray.</value>
        ToolBarTray ToolBarTray { get; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if successfully added, <c>false</c> otherwise</returns>
        string Add(AbstractCommandable item);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="GUID">The GUID.</param>
        /// <returns><c>true</c> if successfully removed, <c>false</c> otherwise</returns>
        bool Remove(string GUID);

        /// <summary>
        /// Gets the specified toolbar using the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>AbstractCommandable.</returns>
        AbstractCommandable Get(string key);

        /// <summary>
        /// Gets the right click menu.
        /// </summary>
        /// <value>The right click menu.</value>
        AbstractMenuItem RightClickMenu { get; }
    }
}