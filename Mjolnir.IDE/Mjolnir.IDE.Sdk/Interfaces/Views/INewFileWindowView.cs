using Mjolnir.IDE.Sdk.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Views
{
    public interface INewFileWindowView
    {
        NewContentAttribute NewContent { get; set; }
    }
}
