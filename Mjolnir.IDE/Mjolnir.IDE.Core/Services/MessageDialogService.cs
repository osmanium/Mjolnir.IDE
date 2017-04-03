using Mjolnir.IDE.Sdk.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mjolnir.IDE.Sdk.Enums;
using System.Windows;

namespace Mjolnir.IDE.Core.Services
{
    public class DefaultMessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowYesNoMessageBox(string title, string message)
        {
            return MessageBox.Show(message, title,MessageBoxButton.YesNo) == MessageBoxResult.Yes 
                ? MessageDialogResult.Yes 
                : MessageDialogResult.No;
        }

        public MessageDialogResult ShowOkMessageBox(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK);
            return MessageDialogResult.OK;
        }

        public MessageDialogResult ShowOkCancelMessageBox(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButton.OKCancel) == MessageBoxResult.OK
                ? MessageDialogResult.OK
                : MessageDialogResult.Cancel;
        }

        public MessageDialogResult ShowYesNoCancelMessageBox(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
