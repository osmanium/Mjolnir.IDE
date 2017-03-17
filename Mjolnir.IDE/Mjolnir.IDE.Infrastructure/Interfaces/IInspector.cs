﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces
{
    public interface IInspector
    {
        string Name { get; }
        bool IsReadOnly { get; }
    }
}
