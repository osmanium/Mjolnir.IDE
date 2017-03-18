using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Infrastructure.Interfaces.Services
{
    public interface IErrorService
    {
        void LogError(ErrorListItem error);
        void RemoveError(Guid errorId);
    }
}
