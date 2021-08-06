using Ninject.Modules;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Utils
{
    public class AdministratorModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdministratorService>().To<AdministratorService>();
        }
    }
}