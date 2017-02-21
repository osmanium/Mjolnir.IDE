using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Interface IOpenDocumentService - used to open a file
    /// </summary>
    public interface IOpenDocumentService
    {
        /// <summary>
        /// Opens the document from the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel Open(object location = null);

        /// <summary>
        /// Opens from content from an ID.
        /// </summary>
        /// <param name="contentID">The content ID.</param>
        /// <param name="makeActive">if set to <c>true</c> makes the new document as the active document.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel OpenFromID(string contentID, bool makeActive = false);
    }
}