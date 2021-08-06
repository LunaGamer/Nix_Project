using AutoMapper;
using Nix_Hotel.API.Models;
using Nix_Hotel.API.Utils;
using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Nix_Hotel.API.Controllers
{
    public class ClientController : ApiController
    {
        private IClientService service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ClientController(IClientService service)
        {
            this.service = service;
        }

        [ResponseType(typeof(List<ClientModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                logger.Info("|CLIENT| Selecting the list of all clients");
                var clientsDTO = service.GetAllClients();
                if (clientsDTO!= null && clientsDTO.Any())
                {
                    logger.Info("|CLIENT| List of all clients was succesfully selected from DB");
                    var clients = HotelMapperAPI.MapClientReadList(clientsDTO);
                    return request.CreateResponse(HttpStatusCode.OK, clients);
                }
                logger.Warn("|CLIENT| List of clients was Empty");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }

        }

        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                logger.Info($"|CLIENT| Selecting client with id = {id}");
                ClientDTO clientDTO = service.Get(id);
                if (clientDTO != null)
                {
                    logger.Info($"|CLIENT| Client with id = {id} was succesfully selected from DB");
                    var client = HotelMapperAPI.MapClientReadSingle(clientDTO);
                    return request.CreateResponse(HttpStatusCode.OK, client);
                }
                logger.Info($"|CLIENT| Client with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|CLIENT| Client with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] ClientModel value)
        {
            try
            {
                logger.Info("|CLIENT| Trying to create new client");
                if (value != null)
                {
                    logger.Info("|CLIENT| New client was succesfully createed");
                    var newClient = HotelMapperAPI.MapClientWrite(value);
                    newClient.Id = 0;
                    int id = service.GetAllClients().Count() + 1;
                    service.Create(newClient);
                    return Get(request, id);
                }
                logger.Info($"|CLIENT| Can't create client");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|CLIENT| Can't create client");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] ClientModel value)
        {
            try
            {
                logger.Info($"|CLIENT| Selecting client with id = {id} for editing");
                var oldClient = service.Get(id);
                if (oldClient != null && value != null)
                {
                    logger.Info($"|CLIENT| Editing client with id = {id}");
                    var newClient = HotelMapperAPI.MapClientWrite(value);
                    newClient.Id = id;
                    service.Update(id, newClient);
                    logger.Info($"|CLIENT| Client with id = {id} edited succesfully");
                    return Get(request, id);
                }
                logger.Info($"|CLIENT| Client with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|CLIENT| Client with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
