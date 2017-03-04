using Mjolnir.IDE.Infrastructure.Enums;
using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Modules.Error.Models
{
    public class ErrorListItem : ValidatableBindableBase
    {
        private ErrorListItemType _itemType;
        public ErrorListItemType ItemType
        {
            get { return _itemType; }
            set
            {
                SetProperty(ref _itemType, value);
            }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                SetProperty(ref _number, value);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
            }
        }

        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                SetProperty(ref _path, value);
                OnPropertyChanged(() => File);
            }
        }

        public string File
        {
            get { return System.IO.Path.GetFileName(Path); }
        }

        private int? _line;
        public int? Line
        {
            get { return _line; }
            set
            {
                SetProperty(ref _line, value);
            }
        }

        private int? _column;
        public int? Column
        {
            get { return _column; }
            set
            {
                SetProperty(ref _column, value);
            }
        }

        public System.Action OnClick { get; set; }

        public ErrorListItem(ErrorListItemType itemType, int number, string description,
            string path = null, int? line = null, int? column = null)
        {
            _itemType = itemType;
            _number = number;
            _description = description;
            _path = path;
            _line = line;
            _column = column;
        }

        public ErrorListItem()
        {

        }
    }
}
