using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Mjolnir.IDE.Sdk.Interfaces.Views
{
    public interface ISplashScreenView
    {
        void Show();

        bool Activate();

        void Close();
        
        void Hide();

        Dispatcher Dispatcher { get; }

        object DataContext { get; set; }
    }
}
