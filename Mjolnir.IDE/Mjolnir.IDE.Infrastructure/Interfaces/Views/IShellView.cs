using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Views
{
    public interface IShellView
    {
        /// <summary>
        /// Closes this instance of the shell.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows this instance of the shell
        /// </summary>
        void Show();

        /// <summary>
        /// Loads the layout of the shell from previous run.
        /// </summary>
        void LoadLayout();

        /// <summary>
        /// Saves the layout of the shell.
        /// </summary>
        void SaveLayout();


        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <value>The top.</value>
        double Top { get; set; }

        /// <summary>
        /// Gets the left.
        /// </summary>
        /// <value>The left.</value>
        double Left { get; set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        double Width { get; set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        double Height { get; set; }

        WindowState WindowState { get; set; }

        object DataContext { get; set; }
    }
}
