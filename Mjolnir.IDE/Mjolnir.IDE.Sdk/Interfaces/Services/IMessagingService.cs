using Mjolnir.IDE.Sdk.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowYesNoMessageBox(string title, string message);
        MessageDialogResult ShowOkMessageBox(string title, string message);
        MessageDialogResult ShowOkCancelMessageBox(string title, string message);
        MessageDialogResult ShowYesNoCancelMessageBox(string title, string message);
    }
}
