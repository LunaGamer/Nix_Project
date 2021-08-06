using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.WEB.Models;
using Nix_Hotel.WEB.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Nix_Hotel.WEB.Controllers
{
    public class AdminController : Controller
    {
        private IAdministratorService service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AdminController(IAdministratorService service)
        {
            this.service = service;
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdministratorModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = HotelMapperWEB.mapperAdministrator.Map<AdministratorModel, AdministratorDTO>(model);
                logger.Info($"|LOGIN| Trying to Log In as administrator with name {admin.Login} ");
                var login = service.Login(admin);
                if (login != null)
                {
                    logger.Info($"|LOGIN| Logged in succesfully as {login.Login} ");
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    logger.Info("|LOGIN| Administrator not found");
                    ModelState.AddModelError("", "Administrator not found");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            logger.Info("|LOGIN| Logged out");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}