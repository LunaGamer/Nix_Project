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
    public class BookingController : ApiController
    {
        private IBookingService service;
        private IRoomService roomService;
        private IMapper mapperRead;
        private IMapper mapperWrite;
        private IMapper mapperRoomRead;
        private IMapper mapperRoomWrite;
        private IMapper mapperCategory;
        private IMapper mapperClient;
        private IMapper mapperStatus;

        public BookingController(IBookingService service, IRoomService roomService)
        {
            this.service = service;
            this.roomService = roomService;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, BookingModel>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<BookingModel, BookingDTO>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperRoomRead = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, RoomModel>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperRoomWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomModel, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap()).CreateMapper();
            mapperClient = new MapperConfiguration(cfg => cfg.CreateMap<ClientDTO, ClientModel>().ReverseMap()).CreateMapper();
            mapperStatus = new MapperConfiguration(cfg => cfg.CreateMap<StatusDTO, StatusModel>().ReverseMap()).CreateMapper();
        }

        [ResponseType(typeof(List<BookingModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                var bookingsDTO = service.GetAllBookings();
                if (bookingsDTO != null && bookingsDTO.Any())
                {
                    List<BookingModel> bookings = new List<BookingModel>();
                    foreach (var bookingDTO in bookingsDTO)
                    {
                        var booking = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
                        booking.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
                        booking.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
                        booking.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
                        booking.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
                        bookings.Add(booking);
                    }
                    return request.CreateResponse(HttpStatusCode.OK, bookings);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                BookingDTO bookingDTO = service.Get(id);
                if (bookingDTO != null)
                {
                    var booking = mapperRead.Map<BookingDTO, BookingModel>(bookingDTO);
                    booking.Room = mapperRoomRead.Map<RoomDTO, RoomModel>(bookingDTO.Room);
                    booking.Room.Category = mapperCategory.Map<CategoryDTO, CategoryModel>(bookingDTO.Room.Category);
                    booking.Client = mapperClient.Map<ClientDTO, ClientModel>(bookingDTO.Client);
                    booking.Status = mapperStatus.Map<StatusDTO, StatusModel>(bookingDTO.Status);
                    return request.CreateResponse(HttpStatusCode.OK, booking);
                }

                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(decimal))]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri] int Year, [FromUri] int Month)
        {
            try
            {
                if ((Month > 0 || Month <= 12) && Year >= 2000 && Year <= 2100)
                {
                    var date = new DateTime(Year, Month, 1);
                    var gane = service.Gain(date);
                    return request.CreateResponse(HttpStatusCode.OK, gane);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

            [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] BookingModel value)
        {
            try
            {
                if (value != null)
                {
                    var bookingDTO = mapperWrite.Map<BookingModel, BookingDTO>(value);
                    bookingDTO.Room = mapperRoomWrite.Map<RoomModel, RoomDTO>(value.Room);
                    bookingDTO.Room.Category = mapperCategory.Map<CategoryModel, CategoryDTO>(value.Room.Category);
                    bookingDTO.Client = mapperClient.Map<ClientModel, ClientDTO>(value.Client);
                    bookingDTO.Status = mapperStatus.Map<StatusModel, StatusDTO>(value.Status);
                    var freeRooms = roomService.GetFreeRooms(bookingDTO.ArrivalDate, bookingDTO.CheckoutDate).ToList();
                    if (freeRooms.Find(x => x.Id == bookingDTO.Room.Id) != null)
                    {
                        service.Create(bookingDTO);
                        return request.CreateResponse(HttpStatusCode.Created, value);
                    }
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] BookingModel value)
        {
            try
            {
                var oldBooking = service.Get(id);
                if (oldBooking!= null && value != null)
                {
                    var newBooking = mapperWrite.Map<BookingModel, BookingDTO>(value);
                    newBooking.Room = mapperRoomWrite.Map<RoomModel, RoomDTO>(value.Room);
                    newBooking.Room.Category = mapperCategory.Map<CategoryModel, CategoryDTO>(value.Room.Category);
                    newBooking.Client = mapperClient.Map<ClientModel, ClientDTO>(value.Client);
                    newBooking.Status = mapperStatus.Map<StatusModel, StatusDTO>(value.Status);
                    service.Update(id, newBooking);
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
