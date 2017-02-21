using Microsoft.Practices.Unity;
using Mjolnir.IDE.Infrastructure.Interfaces;
using Mjolnir.IDE.Infrastructure.Interfaces.Services;
using Mjolnir.IDE.Infrastructure.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mjolnir.IDE.Test
{
    public class CoreModule : IApplicationDefinition
    {
        private IUnityContainer _container;

        public CoreModule(IUnityContainer container)
        {

            _container = container;

            LoadCommands();
            LoadMenus();

        }

        public void LoadMenus()
        {
            var menuService = _container.Resolve<IMenuService>();
            menuService.Add(new MenuItemViewModel("_File", 1));
        }

        public void LoadSettings()
        {
        }

        public void LoadTheme()
        {
        }

        public void LoadToolbar()
        {
        }

        public void RegisterParts()
        {
        }

        public void LoadCommands()
        {
        }
    }
}
