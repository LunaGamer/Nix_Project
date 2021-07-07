using AutoMapper;
using Nix_Hotel.API.Models;
using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
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
        private IMapper mapperRead;
        private IMapper mapperWrite;

        public ClientController(IClientService service)
        {
            this.service = service;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>()).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<ClientModel, ClientDTO>()).CreateMapper();
        }

        [ResponseType(typeof(List<ClientModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var clientsDTO = service.GetAllClients();
                if (clientsDTO!= null && clientsDTO.Any())
                {
                    var clients = mapperRead.Map<IEnumerable<ClientDTO>, List<ClientModel>>(clientsDTO);
                    return request.CreateResponse(HttpStatusCode.OK, clients);
                }
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
                ClientDTO clientDTO = service.Get(id);
                var client = new ClientModel();

                if (clientDTO != null)
                {
                    client = mapperRead.Map<ClientDTO, ClientModel>(clientDTO);
                    return request.CreateResponse(HttpStatusCode.OK, client);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] ClientModel value)
        {
            try
            {
                if (value != null)
                {
                    var newClient = mapperWrite.Map<ClientModel, ClientDTO>(value);
                    service.Create(newClient);
                    return Get(request, newClient.Id);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(ClientModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] ClientModel value)
        {
            try
            {
                var oldClient = service.Get(id);
                if (oldClient != null && value != null)
                {
                    var newClient = mapperWrite.Map<ClientModel, ClientDTO>(value);
                    service.Update(id, newClient);
                    return Get(request, id);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public void Delete(int id)
        {

        }
    }
}
