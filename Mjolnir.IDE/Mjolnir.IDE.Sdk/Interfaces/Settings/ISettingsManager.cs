using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mjolnir.IDE.Sdk.Interfaces.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Gets the settings command.
        /// </summary>
        /// <value>The settings.</value>
        ICommand SettingsCommand { get; }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The GUID for the item added which needs to be used to remove the item</returns>
        string Add(DefaultSettingsItem item);

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="GuidString">The unique GUID set for the menu available for the creator.</param>
        /// <returns><c>true</c> if successfully removed, <c>false</c> otherwise</returns>
        bool Remove(string GuidString);

        /// <summary>
        /// Gets the node with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>`0.</returns>
        DefaultSettingsItem Get(string key);
    }
}