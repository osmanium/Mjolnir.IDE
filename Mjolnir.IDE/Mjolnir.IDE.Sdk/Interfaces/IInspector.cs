﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces
{
    public interface IInspector
    {
        string Name { get; }
        bool IsReadOnly { get; }
    }
}
