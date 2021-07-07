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
    public class RoomController : ApiController
    {
        private IRoomService service;
        private IMapper mapperRead;
        private IMapper mapperWrite;
        private IMapper mapperCategory;

        public RoomController(IRoomService service)
        {
            this.service = service;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomModel>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap()).CreateMapper();
        }

        [ResponseType(typeof(List<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var roomsDTO = service.GetAllRooms();
                if (roomsDTO.Any())
                {
                    List<RoomModel> rooms = new List<RoomModel>();
                    foreach (var roomDTO in roomsDTO)
                    {
                        var room = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
                        room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                        rooms.Add(room);
                    }
                    return request.CreateResponse(HttpStatusCode.OK, rooms);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                RoomDTO roomDTO = service.Get(id);
                var room = new RoomModel();

                if (roomDTO != null)
                {
                    room = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
                    room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                    return request.CreateResponse(HttpStatusCode.OK, room);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(List<RoomModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri] string startDate, [FromUri] string endDate)
        {
            try
            {
                DateTime start = DateTime.Parse(startDate);
                DateTime end = DateTime.Parse(endDate);
                if (start != null && end != null && start < end)
                {
                    var roomsDTO = service.GetFreeRooms(start, end);
                    if (roomsDTO!= null && roomsDTO.Any())
                    {
                        List<RoomModel> rooms = new List<RoomModel>();
                        foreach (var roomDTO in roomsDTO)
                        {
                            var room = mapperRead.Map<RoomDTO, RoomModel>(roomDTO);
                            room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(roomDTO.Category);
                            rooms.Add(room);
                        }
                        return request.CreateResponse(HttpStatusCode.OK, rooms);
                    }
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (FormatException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(RoomModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] RoomModel value)
        {
            try
            {
                var oldRoom = service.Get(id);
                if (oldRoom != null && value != null)
                {
                    var newRoom = mapperWrite.Map<RoomModel, RoomDTO>(value);
                    newRoom.Category = mapperCategory.Map<CategoryModel, CategoryDTO>(value.Category);
                    service.Update(id, newRoom);
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
