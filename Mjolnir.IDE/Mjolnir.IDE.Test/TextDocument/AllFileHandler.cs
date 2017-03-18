using Microsoft.Practices.Unity;
using Microsoft.Win32;
using Mjolnir.IDE.Sdk.Attributes;
using Mjolnir.IDE.Sdk.Interfaces.Services;
using Mjolnir.IDE.Sdk.ViewModels;
using Mjolnir.IDE.Test.TextDocument.Model;
using Mjolnir.IDE.Test.TextDocument.View;
using Mjolnir.IDE.Test.TextDocument.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.TextDocument
{
    /// <summary>
    /// AllFileHandler class that supports opening of any file on the computer
    /// </summary>
    [FileContent("All files", "*.*", 10000)]
    [NewContent("Text file", 10000, "Creates a basic text file", "pack://application:,,,/Mjolnir.IDE.Test;component/Icons/Textfile.png")]
    public class AllFileHandler : IContentHandler
    {
        /// <summary>
        /// The injected container
        /// </summary>
        private readonly IUnityContainer _container;

        /// <summary>
        /// The injected logger service
        /// </summary>
        private readonly IOutputService _outputService;

        /// <summary>
        /// The save file dialog
        /// </summary>
        private SaveFileDialog _dialog;

        /// <summary>
        /// Constructor of AllFileHandler - all parameters are injected
        /// </summary>
        /// <param name="container">The injected container of the application</param>
        /// <param name="outputService">The injected logger service of the application</param>
        public AllFileHandler(IUnityContainer container, IOutputService outputService)
        {
            _container = container;
            _outputService = outputService;
            _dialog = new SaveFileDialog();
        }

        #region IContentHandler Members

        public ContentViewModel NewContent(object parameter)
        {
            var vm = _container.Resolve<TextViewModel>();
            var model = _container.Resolve<TextModel>();
            var view = _container.Resolve<TextView>();

            _outputService.LogOutput("Creating a new simple file using AllFileHandler", OutputCategory.Info, OutputPriority.Low);

            //Clear the undo stack
            model.Document.UndoStack.ClearAll();

            //Set the model and view
            vm.Model = model;
            vm.View = view;
            vm.Title = "untitled";
            vm.View.DataContext = model;
            vm.Handler = this;
            vm.Model.IsDirty = true;

            return vm;
        }

        /// <summary>
        /// Validates the content by checking if a file exists for the specified location
        /// </summary>
        /// <param name="info">The string containing the file location</param>
        /// <returns>True, if the file exists - false otherwise</returns>
        public bool ValidateContentType(object info)
        {
            var location = info as string;
            if (location != null)
            {
                return File.Exists(location);
            }
            return false;
        }

        /// <summary>
        /// Validates the content from an ID - the ContentID from the ContentViewModel
        /// </summary>
        /// <param name="contentId">The content ID which needs to be validated</param>
        /// <returns>True, if valid from content ID - false, otherwise</returns>
        public bool ValidateContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Opens a file and returns the corresponding ContentViewModel
        /// </summary>
        /// <param name="info">The string location of the file</param>
        /// <returns>The <see cref="TextViewModel"/> for the file.</returns>
        public ContentViewModel OpenContent(object info)
        {
            var location = info as string;
            if (location != null)
            {
                var vm = _container.Resolve<TextViewModel>();
                var model = _container.Resolve<TextModel>();
                var view = _container.Resolve<TextView>();

                //Model details
                model.SetLocation(info);
                try
                {
                    model.Document.Text = File.ReadAllText(location);
                    model.IsDirty = false;
                }
                catch (Exception exception)
                {
                    _outputService.LogOutput(exception.Message, OutputCategory.Exception, OutputPriority.High);
                    _outputService.LogOutput(exception.StackTrace, OutputCategory.Exception, OutputPriority.High);
                    return null;
                }

                //Clear the undo stack
                model.Document.UndoStack.ClearAll();

                //Set the model and view
                vm.Model = model;
                vm.View = view;
                vm.Title = Path.GetFileName(location);
                vm.View.DataContext = model;

                return vm;
            }
            return null;
        }

        /// <summary>
        /// Opens the content from the content ID
        /// </summary>
        /// <param name="contentId">The content ID</param>
        /// <returns></returns>
        public ContentViewModel OpenContentFromId(string contentId)
        {
            string[] split = Regex.Split(contentId, ":##:");
            if (split.Count() == 2)
            {
                string identifier = split[0];
                string path = split[1];
                if (identifier == "FILE" && File.Exists(path))
                {
                    return OpenContent(path);
                }
            }
            return null;
        }

        /// <summary>
        /// Saves the content of the TextViewModel
        /// </summary>
        /// <param name="contentViewModel">This needs to be a TextViewModel that needs to be saved</param>
        /// <param name="saveAs">Pass in true if you need to Save As?</param>
        /// <returns>true, if successful - false, otherwise</returns>
        public virtual bool SaveContent(ContentViewModel contentViewModel, bool saveAs = false)
        {
            var textViewModel = contentViewModel as TextViewModel;

            if (textViewModel == null)
            {
                _outputService.LogOutput("ContentViewModel needs to be a TextViewModel to save details", OutputCategory.Exception,
                                   OutputPriority.High);
                throw new ArgumentException("ContentViewModel needs to be a TextViewModel to save details");
            }

            var textModel = textViewModel.Model as TextModel;

            if (textModel == null)
            {
                _outputService.LogOutput("TextViewModel does not have a TextModel which should have the text",
                                   OutputCategory.Exception, OutputPriority.High);
                throw new ArgumentException("TextViewModel does not have a TextModel which should have the text");
            }

            var location = textModel.Location as string;

            if (location == null)
            {
                //If there is no location, just prompt for Save As..
                saveAs = true;
            }

            if (saveAs)
            {
                if (location != null)
                    _dialog.InitialDirectory = Path.GetDirectoryName(location);

                _dialog.CheckPathExists = true;
                _dialog.DefaultExt = "txt";
                _dialog.Filter = "All files (*.*)|*.*";

                if (_dialog.ShowDialog() == true)
                {
                    location = _dialog.FileName;
                    textModel.SetLocation(location);
                    textViewModel.Title = Path.GetFileName(location);
                    try
                    {
                        File.WriteAllText(location, textModel.Document.Text);
                        textModel.IsDirty = false;
                        return true;
                    }
                    catch (Exception exception)
                    {
                        _outputService.LogOutput(exception.Message, OutputCategory.Exception, OutputPriority.High);
                        _outputService.LogOutput(exception.StackTrace, OutputCategory.Exception, OutputPriority.High);
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    File.WriteAllText(location, textModel.Document.Text);
                    textModel.IsDirty = false;
                    return true;
                }
                catch (Exception exception)
                {
                    _outputService.LogOutput(exception.Message, OutputCategory.Exception, OutputPriority.High);
                    _outputService.LogOutput(exception.StackTrace, OutputCategory.Exception, OutputPriority.High);
                    return false;
                }
            }

            return false;
        }

        #endregion
    }
}
