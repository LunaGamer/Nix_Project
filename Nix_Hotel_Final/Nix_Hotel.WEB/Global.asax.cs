using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using Nix_Hotel.BLL.Infrastructure;
using Nix_Hotel.WEB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Nix_Hotel.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule clientModule = new ClientModule();
            NinjectModule roomModule = new RoomModule();
            NinjectModule bookingModule = new BookingModule();
            NinjectModule administratorModule = new AdministratorModule();
            NinjectModule dependencyModule = new DependencyModule("NixHotel");
            var kernel = new StandardKernel(clientModule, roomModule, bookingModule, administratorModule, dependencyModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
