using Ninject.Modules;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Utils
{
    public class BookingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookingService>().To<BookingService>();
        }
    }
}