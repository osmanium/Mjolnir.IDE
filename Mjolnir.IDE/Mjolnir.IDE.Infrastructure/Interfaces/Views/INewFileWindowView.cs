using Mjolnir.IDE.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Views
{
    public interface INewFileWindowView
    {
        NewContentAttribute NewContent { get; set; }
    }
}
