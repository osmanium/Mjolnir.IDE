using Mjolnir.IDE.Sdk.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace Mjolnir.IDE.Core.Services
{
    public class DefaultFileDialogService : IFileDialogService
    {
        private readonly IMessageDialogService _iMessageDialogService;
        private OpenFileDialog _openFileDialog;
        private SaveFileDialog _saveFileDialog;

        public bool CheckPathExists { get; set; }
        public string DefaultExt { get; set; }
        public string[] FileNames { get; set; }
        public string FileName { get; set; }
        public string Filter { get; set; }
        public string InitialDirectory { get; set; }
        public string Title { get; set; }


        public DefaultFileDialogService(IMessageDialogService iMessageDialogService)
        {
            this._iMessageDialogService = iMessageDialogService;

            _openFileDialog = new OpenFileDialog();
            _saveFileDialog = new SaveFileDialog();
        }

        public void SetConfiguraion(bool checkPathExists, string defaultExt, string filter, string initialDirectory, string title)
        {
            _saveFileDialog.CheckPathExists = _openFileDialog.CheckPathExists = this.CheckPathExists = checkPathExists;
            _saveFileDialog.DefaultExt = _openFileDialog.DefaultExt = this.DefaultExt = defaultExt;
            _saveFileDialog.Filter = _openFileDialog.Filter = this.Filter = filter;
            _saveFileDialog.InitialDirectory = _openFileDialog.InitialDirectory = this.InitialDirectory = initialDirectory;
            _saveFileDialog.Title = _openFileDialog.Title = this.Title = title;
        }



        #region Save File Dialog
        public string ShowSaveFileDialog()
        {
            string filePath = string.Empty;
            
            var result = _saveFileDialog.ShowDialog();
            if (result != null && result == true)
            {
                filePath = _saveFileDialog.FileName;
            }

            return filePath;
        }
        #endregion

        #region Open File Dialog
        public string ShowSelectFileDialog()
        {
            var result = _openFileDialog.ShowDialog();
            if (result != null && result == true)
            {
                return _openFileDialog.FileName;
            }
            return null;
        }
        #endregion
    }
}
