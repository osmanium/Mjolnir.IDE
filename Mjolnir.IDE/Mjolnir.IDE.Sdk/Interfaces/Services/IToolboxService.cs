using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Sdk.Interfaces.Services
{
    public interface IToolboxService
    {
        List<ToolboxItem> GetToolboxItems(string documentTypeName);

        void AddToolboxItems(string documentTypeName, List<ToolboxItem> newItems);

        void RemoveToolboxItems(string documentTypeName, List<ToolboxItem> newItems);

        void RefreshToolboxItems();
    }
}
