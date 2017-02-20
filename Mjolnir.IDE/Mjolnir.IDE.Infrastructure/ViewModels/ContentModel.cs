using Mjolnir.UI.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.ViewModels
{
    [DataContract]
    [Serializable]
    public abstract class ContentModel : ValidatableBindableBase
    {
        protected bool _isDirty;
        protected object _location;

        /// <summary>
        /// The document location - could be a file location/server object etc.
        /// </summary>
        [Browsable(false)]
        public virtual object Location
        {
            get { return _location; }
            protected set { SetProperty(ref _location, value); }
        }

        /// <summary>
        /// Is the document dirty - does it need to be saved?
        /// </summary>
        [Browsable(false)]
        public virtual bool IsDirty
        {
            get { return _isDirty; }
            protected internal set
            {
                SetProperty(ref _isDirty, value);
            }
        }
    }
}