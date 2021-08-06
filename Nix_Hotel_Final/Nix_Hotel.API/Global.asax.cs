using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Nix_Hotel.API.Utils;
using Nix_Hotel.BLL.Infrastructure;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.Filter;

namespace Nix_Hotel.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule clientModule = new ClientModule();
            NinjectModule roomModule = new RoomModule();
            NinjectModule bookingModule = new BookingModule();
            NinjectModule dependencyModule = new DependencyModule("NixHotel");
            var kernel = new StandardKernel(clientModule, roomModule, bookingModule, dependencyModule);
            kernel.Bind<DefaultFilterProviders>().ToSelf().WithConstructorArgument(GlobalConfiguration.Configuration.Services.GetFilterProviders());
            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));
            GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
        }
    }
}
