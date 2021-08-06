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
    public class RoomController : ApiController
    {
        private IRoomService service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RoomController(IRoomService service)
        {
            this.service = service;
        }

        [ResponseType(typeof(List<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                logger.Info("|ROOM| Selecting the list of all rooms");
                var roomsDTO = service.GetAllRooms();
                if (roomsDTO.Any())
                {
                    logger.Info("|ROOM| List of all rooms was succesfully selected from DB");
                    List<RoomModel> rooms = HotelMapperAPI.MapRoomReadList(roomsDTO);
                    return request.CreateResponse(HttpStatusCode.OK, rooms);
                }
                logger.Warn("|CLIENT| List of rooms was Empty");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Warn("|CLIENT| List of rooms was Empty");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                logger.Info($"|ROOM| Selecting room with id = {id}");
                RoomDTO roomDTO = service.Get(id);
                var room = HotelMapperAPI.MapRoomReadSingle(roomDTO);
                if (room != null)
                {
                    logger.Info($"|ROOM| Room with id = {id} was succesfully selected from DB");
                    return request.CreateResponse(HttpStatusCode.OK, room);
                }
                logger.Info($"|ROOM| Room with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|ROOM| Room with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(List<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri] string startDate, [FromUri] string endDate)
        {
            try
            {
                logger.Info("|ROOM| Trying to find free rooms");
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);
                if (start != null && end != null && start < end)
                {
                    logger.Info("|ROOM| Free rooms found");
                    var roomsDTO = service.GetFreeRooms(start, end);
                    List<RoomModel> rooms = HotelMapperAPI.MapRoomReadList(roomsDTO);                    
                    return request.CreateResponse(HttpStatusCode.OK, rooms);                    
                }
                logger.Info("|ROOM| Couldn't find free rooms");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (FormatException ex)
            {
                logger.Info($"|ROOM| Incorrect dates format");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                logger.Info("|ROOM| Couldn't find free rooms"); 
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] RoomModel value)
        {
            try
            {
                logger.Info("|ROOM| Trying to create new room");
                if (value != null)
                {
                    logger.Info("|ROOM| New room was succesfully createed");
                    var newRoom = HotelMapperAPI.MapRoomWrite(value);
                    int id = service.GetAllRooms().Count() + 1;
                    service.Create(newRoom);
                    return Get(request, id);
                }
                logger.Info("|ROOM| Can't create empty room");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|ROOM| Can't create room");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] RoomModel value)
        {
            try
            {
                logger.Info($"|ROOM| Selecting room with id = {id} for editing");
                var oldRoom = service.Get(id);
                if (oldRoom != null && value != null)
                {
                    logger.Info($"|ROOM| Editing room with id = {id}");
                    var newRoom = HotelMapperAPI.MapRoomWrite(value);
                    service.Update(id, newRoom);
                    logger.Info($"|ROOM| Room with id = {id} edited succesfully");
                    return Get(request, id);
                }
                logger.Info($"|ROOM| Room with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|ROOM| Room with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
