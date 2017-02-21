﻿using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Mjolnir.IDE.Core.Services.Attributes;
using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Events;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.Settings;
using Mjolnir.IDE.Infrastructure.ViewModels;
using Mjolnir.IDE.Modules.Settings;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Core.Services
{
    /// <summary>
    /// The open file service
    /// </summary>
    public sealed class OpenDocumentService : IOpenDocumentService
    {
        /// <summary>
        /// The injected container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// The injected event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The injected logger
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// The Open file dialog
        /// </summary>
        private OpenFileDialog _dialog;

        /// <summary>
        /// The workspace
        /// </summary>
        private AbstractWorkspace _workspace;

        /// <summary>
        /// The content handler registry
        /// </summary>
        private ContentHandlerRegistry _handler;

        /// <summary>
        /// The recent settings
        /// </summary>
        private RecentViewSettings _recentSettings;

        /// <summary>
        /// Constructor for Open file service
        /// </summary>
        /// <param name="container">The injected container</param>
        /// <param name="eventAggregator">The injected event aggregator</param>
        /// <param name="logger">The injected logger</param>
        public OpenDocumentService(IUnityContainer container, IEventAggregator eventAggregator, ILoggerService logger,
                                   AbstractWorkspace workspace, IContentHandlerRegistry handler,
                                   IRecentViewSettings recentSettings)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _dialog = new OpenFileDialog();
            _workspace = workspace;
            _handler = handler as ContentHandlerRegistry;
            _recentSettings = recentSettings as RecentViewSettings;
        }

        #region IOpenDocumentService Members

        /// <summary>
        /// Opens the object - if object is null, show a open file dialog to select a file to open
        /// </summary>
        /// <param name="location">The optional object to open</param>
        /// <returns>A document which was added to the workspace as a content view model</returns>
        public ContentViewModel Open(object location = null)
        {
            bool? result;
            ContentViewModel returnValue = null;

            if (location == null)
            {
                _dialog.Filter = "";
                string sep = "";
                var attributes =
                    _handler.ContentHandlers.SelectMany(
                        handler =>
                        (FileContentAttribute[])
                        (handler.GetType()).GetCustomAttributes(typeof(FileContentAttribute), true)).ToList();
                attributes.Sort((attribute, contentAttribute) => attribute.Priority - contentAttribute.Priority);
                foreach (var contentAttribute in attributes)
                {
                    _dialog.Filter = String.Format("{0}{1}{2} ({3})|{3}", _dialog.Filter, sep, contentAttribute.Display,
                                                   contentAttribute.Extension);
                    sep = "|";
                }

                result = _dialog.ShowDialog();
                location = _dialog.FileName;
            }
            else
            {
                result = true;
            }

            if (result == true && !string.IsNullOrWhiteSpace(location.ToString()))
            {
                //Let the handler figure out which view model to return
                if (_handler != null)
                {
                    ContentViewModel openValue = _handler.GetViewModel(location);

                    if (openValue != null)
                    {
                        //Check if the document is already open
                        foreach (ContentViewModel contentViewModel in _workspace.Documents)
                        {
                            if (contentViewModel.Model.Location != null)
                            {
                                if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                                {
                                    _logger.Log(
                                        "Document " + contentViewModel.Model.Location +
                                        "already open - making it active",
                                        LogCategory.Info, LogPriority.Low);
                                    _workspace.ActiveDocument = contentViewModel;
                                    return contentViewModel;
                                }
                            }
                        }

                        _logger.Log("Opening file" + location + " !!", LogCategory.Info, LogPriority.Low);

                        // Publish the event to the Application - subscribers can use this object
                        _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                        //Add it to the actual workspace
                        _workspace.Documents.Add(openValue);

                        //Make it the active document
                        _workspace.ActiveDocument = openValue;

                        //Add it to the recent documents opened
                        _recentSettings.Update(openValue);

                        returnValue = openValue;
                    }
                    else
                    {
                        _logger.Log("Unable to find a IContentHandler to open " + location, LogCategory.Error,
                                    LogPriority.High);
                    }
                }
            }
            else
            {
                _logger.Log("Canceled out of open file dialog", LogCategory.Info, LogPriority.Low);
            }
            return returnValue;
        }

        /// <summary>
        /// Opens the contentID
        /// </summary>
        /// <param name="contentID">The contentID to open</param>
        /// <param name="makeActive">if set to <c>true</c> makes the new document as the active document.</param>
        /// <returns>A document which was added to the workspace as a content view model</returns>
        public ContentViewModel OpenFromID(string contentID, bool makeActive = false)
        {
            //Let the handler figure out which view model to return
            ContentViewModel openValue = _handler.GetViewModelFromContentId(contentID);

            if (openValue != null)
            {
                //Check if the document is already open
                foreach (ContentViewModel contentViewModel in _workspace.Documents)
                {
                    if (contentViewModel.Model.Location != null)
                    {
                        if (contentViewModel.Model.Location.Equals(openValue.Model.Location))
                        {
                            _logger.Log("Document " + contentViewModel.Model.Location + "already open.",
                                        LogCategory.Info,
                                        LogPriority.Low);

                            if (makeActive)
                                _workspace.ActiveDocument = contentViewModel;

                            return contentViewModel;
                        }
                    }
                }

                _logger.Log("Opening content with " + contentID + " !!", LogCategory.Info, LogPriority.Low);

                // Publish the event to the Application - subscribers can use this object
                _eventAggregator.GetEvent<OpenContentEvent>().Publish(openValue);

                _workspace.Documents.Add(openValue);

                if (makeActive)
                    _workspace.ActiveDocument = openValue;

                //Add it to the recent documents opened
                _recentSettings.Update(openValue);

                return openValue;
            }

            _logger.Log("Unable to find a IContentHandler to open content with ID = " + contentID, LogCategory.Error,
                        LogPriority.High);
            return null;
        }

        #endregion
    }
}