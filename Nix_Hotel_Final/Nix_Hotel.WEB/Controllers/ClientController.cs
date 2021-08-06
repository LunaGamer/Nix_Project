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

namespace Nix_Hotel.WEB.Controllers
{
    public class ClientController : Controller
    {
        private IClientService service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ClientController(IClientService service)
        {
            this.service = service;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewAll()
        {
            logger.Info("|CLIENT| Selecting the list of all clients");
            var clients = HotelMapperWEB.MapClientReadList(service.GetAllClients());
            if (clients != null)
            {
                logger.Info("|CLIENT| List of all clients was succesfully selected from DB");
                return View(clients);
            }
            logger.Warn("|CLIENT| List of clients was Empty");
            return RedirectToAction("Error", "Client");
        }

        [Authorize]
        public ActionResult View(int id)
        {
            logger.Info($"|CLIENT| Selecting client with id = {id}");
            var client = HotelMapperWEB.MapClientReadSingle(service.Get(id));
            if (client != null)
            {
                logger.Info($"|CLIENT| Client with id = {id} was succesfully selected from DB");
                return View(client);
            }
            logger.Info($"|CLIENT| Client with id = {id} not found");
            return RedirectToAction("Error", "Client");
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ClientModel client)
        {
            logger.Info("|CLIENT| Trying to create new client");
            if (ModelState.IsValid)
            {
                logger.Info("|CLIENT| New client was succesfully createed");
                var newClient = HotelMapperWEB.MapClientWrite(client);
                int id = service.GetAllClients().Count() + 1;
                service.Create(newClient);
                return RedirectToAction("View", "Client", new { id = id });
            }
            logger.Info($"|CLIENT| Can't create client, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            logger.Info($"|CLIENT| Selecting client with id = {id} for editing");
            var client = HotelMapperWEB.mapperClient.Map<ClientDTO, ClientModel>(service.Get(id));
            if (client != null)
            {
                return View(client);
            }
            logger.Info($"|CLIENT| Client with id = {id} not found");
            return RedirectToAction("Error", "Client");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ClientModel client)
        {
            logger.Info($"|CLIENT| Editing client with id = {id}");
            if (ModelState.IsValid)
            {
                logger.Info($"|CLIENT| Client with id = {client.Id} edited succesfully");
                var newClient = HotelMapperWEB.MapClientWrite(client);
                service.Update(client.Id, newClient);
                return RedirectToAction("View", "Client", new { id = client.Id });
            }
            logger.Info($"|CLIENT| Can't edit client, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0l).Errors.FirstOrDefault().ErrorMessage}");
            return View();
        }

    }
}