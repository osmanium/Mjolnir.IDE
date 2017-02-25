using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test.TextDocument.Model
{
    /// <summary>
    /// Class TextModel which contains the text of the document
    /// </summary>
    public class TextModel : ContentModel
    {
        /// <summary>
        /// The command manager
        /// </summary>
        protected readonly ICommandManager _commandManager;

        /// <summary>
        /// The menu service
        /// </summary>
        protected readonly IMenuService _menuService;

        /// <summary>
        /// The old text which was last saved
        /// </summary>
        protected string OldText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextModel" /> class.
        /// </summary>
        /// <param name="commandManager">The injected command manager.</param>
        /// <param name="menuService">The menu service.</param>
        public TextModel(ICommandManager commandManager, IMenuService menuService)
        {
            Document = new ICSharpCode.AvalonEdit.Document.TextDocument();
            _commandManager = commandManager;
            _menuService = menuService;
            Document.PropertyChanged += DocumentPropertyChanged;
            Document.TextChanged += DocumentOnTextChanged;
            OldText = "";

            IsValidationEnabled = false;
        }

        /// <summary>
        /// Is the document dirty - does it need to be saved?
        /// </summary>
        /// <value><c>true</c> if this instance is dirty; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public override bool IsDirty
        {
            get { return base.IsDirty; }
            set
            {
                base.IsDirty = value;
                if (value == false)
                {
                    OldText = Document.Text;
                }
            }
        }

        /// <summary>
        /// Gets or sets the AvalonEdit's text document.
        /// </summary>
        /// <value>The document.</value>
        [Browsable(false)]
        public ICSharpCode.AvalonEdit.Document.TextDocument Document { get; protected set; }

        /// <summary>
        /// Documents the on text changed.
        /// </summary>
        /// <param name="sender">The sender - a text editor instance.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DocumentOnTextChanged(object sender, EventArgs eventArgs)
        {
            IsDirty = (OldText != Document.Text);
        }

        /// <summary>
        /// Documents the property changed.
        /// </summary>
        /// <param name="sender">The sender - a text model instance.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void DocumentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //OnPropertyChanged(() => Document);
        }

        internal void SetLocation(object location)
        {
            this.Location = location;
            OnPropertyChanged(() => Location);
        }
    }
}