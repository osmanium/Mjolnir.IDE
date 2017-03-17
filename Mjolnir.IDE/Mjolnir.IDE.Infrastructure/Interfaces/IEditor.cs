using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces
{
    public interface IEditor : IInspector
    {
        BoundPropertyDescriptor BoundPropertyDescriptor { get; set; }
    }
}