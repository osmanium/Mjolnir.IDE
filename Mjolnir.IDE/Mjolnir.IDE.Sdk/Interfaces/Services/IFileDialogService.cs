using Mjolnir.IDE.Sdk.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Services
{

    public interface IFileDialogService
    {
        bool CheckPathExists { get; set; }
        string DefaultExt { get; set; }
        string[] FileNames { get; }
        string FileName { get; set; }
        string Filter { get; set; }
        string InitialDirectory { get; set; }


        Stream ShowSelectFileDialog(FileMode fileMode);

        string ShowSaveFileDialog(byte[] fileContent);
    }
}
