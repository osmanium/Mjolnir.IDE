﻿using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Services
{
    /// <summary>
    /// Interface IContentHandler
    /// </summary>
    public interface IContentHandler
    {
        /// <summary>
        /// Creates a new content.
        /// </summary>
        /// <param name="parameter">The parameter needed to create a new content.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel NewContent(object parameter);
        ContentViewModel OpenContent(object info);

        /// <summary>
        /// Opens the content from id.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <returns>ContentViewModel.</returns>
        ContentViewModel OpenContentFromId(string contentId);

        /// <summary>
        /// Saves the content.
        /// </summary>
        /// <param name="contentViewModel">The content view model.</param>
        /// <param name="saveAs">if set to <c>true</c> the document needs to be saved as - you need to implement logic for saving the document.</param>
        /// <returns><c>true</c> if successfully saved, <c>false</c> otherwise</returns>
        bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false);

        /// <summary>
        /// Validates the content from id.
        /// </summary>
        /// <param name="contentId">The content id.</param>
        /// <returns><c>true</c> if this handler is able to open the contentID, <c>false</c> otherwise</returns>
        bool ValidateContentFromId(string contentId);

        /// <summary>
        /// Validates the type of the content.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <returns><c>true</c> if this handler is able to open info, <c>false</c> otherwise</returns>
        bool ValidateContentType(object info);
    }
}