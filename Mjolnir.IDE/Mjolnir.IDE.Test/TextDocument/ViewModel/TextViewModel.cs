using Mjolnir.IDE.Infrastructure;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.TextDocument.ViewModel
{
    /// <summary>
    /// Class TextViewModel
    /// </summary>
    public class TextViewModel : ContentViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentViewModel" /> class.
        /// </summary>
        /// <param name="workspace">The injected workspace.</param>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="output">The injected logger.</param>
        /// <param name="menuService">The menu service.</param>
        public TextViewModel(AbstractWorkspace workspace, ICommandManager commandManager, IOutputService output,
                             IMenuService menuService)
            : base(workspace, commandManager, output, menuService)
        {
            IsValidationEnabled = false;
        }

        /// <summary>
        /// The title of the document
        /// </summary>
        /// <value>The tool tip.</value>
        public override string Tooltip
        {
            get { return Model.Location as String; }
            protected set { base.Tooltip = value; }
        }

        /// <summary>
        /// The content ID - unique value for each document. For TextViewModels, the contentID is "FILE:##:" + location of the file.
        /// </summary>
        /// <value>The content id.</value>
        public override string ContentId
        {
            get { return "FILE:##:" + Tooltip; }
        }
    }
}