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
    public class BookingController : ApiController
    {
        private IBookingService service;
        private IRoomService roomService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BookingController(IBookingService service, IRoomService roomService)
        {
            this.service = service;
            this.roomService = roomService;
        }

        [ResponseType(typeof(List<BookingModel>))]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            try
            {
                logger.Info("|BOOKING| Selecting the list of all bookings");
                var bookingsDTO = service.GetAllBookings();
                if (bookingsDTO != null && bookingsDTO.Any())
                {
                    logger.Info("|BOOKING| List of all bookings was succesfully selected from DB");
                    List<BookingModel> bookings = HotelMapperAPI.MapBookingReadList(bookingsDTO);
                    return request.CreateResponse(HttpStatusCode.OK, bookings);
                }
                logger.Warn("|BOOKING| List of bookings was Empty");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Warn("|BOOKING| List of bookings was Empty");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            try
            {
                logger.Info($"|BOOKING| Selecting booking with id = {id}");
                BookingDTO bookingDTO = service.Get(id);
                if (bookingDTO != null)
                {
                    logger.Info($"|BOOKING| Booking with id = {id} was succesfully selected from DB");
                    var booking = HotelMapperAPI.MapBookingReadSingle(bookingDTO);
                    return request.CreateResponse(HttpStatusCode.OK, booking);
                }
                logger.Info($"|BOOKING| Booking with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|BOOKING| Booking with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [ResponseType(typeof(decimal))]
        public HttpResponseMessage Get(HttpRequestMessage request, [FromUri] int Year, [FromUri] int Month)
        {
            try
            {
                logger.Info($"|BOOKING| Trying to calculate gain");
                if ((Month > 0 && Month <= 12) && (Year >= 2000 && Year <= 2100))
                {
                    logger.Info($"|BOOKING| Gain for Year = {Year}, Month = {Month} succesfully calculated");
                    var date = new DateTime(Year, Month, 1);
                    var gane = service.Gain(date);
                    return request.CreateResponse(HttpStatusCode.OK, gane);
                }
                logger.Info($"|BOOKING| Can't calculate gain");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|BOOKING| Can't calculate gain");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

            [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Post(HttpRequestMessage request, [FromBody] BookingModel value)
        {
            try
            {
                logger.Info("|BOOKING| Trying to create new booking");
                if (value != null)
                {
                    var bookingDTO = HotelMapperAPI.MapBookingWrite(value);
                    var freeRooms = roomService.GetFreeRooms(bookingDTO.ArrivalDate, bookingDTO.CheckoutDate).ToList();
                    if (freeRooms.Find(x => x.Id == bookingDTO.Room.Id) != null)
                    {
                        logger.Info("|BOOKING| New booking was succesfully createed");
                        bookingDTO.Id = 0;
                        int id = service.GetAllBookings().Count() + 1;
                        service.Create(bookingDTO);
                        return Get(request, id);
                    }
                }
                logger.Info($"|BOOKING| Can't create booking with such parameters");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|BOOKING| Can't create booking with such parameters");
                return request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [ResponseType(typeof(BookingModel))]
        public HttpResponseMessage Put(HttpRequestMessage request, int id, [FromBody] BookingModel value)
        {
            try
            {
                logger.Info($"|BOOKING| Selecting booking with id = {id} for editing");
                var oldBooking = service.Get(id);
                if (oldBooking!= null && value != null)
                {
                    logger.Info($"|BOOKING| Editing booking with id = {id}");
                    var newBooking = HotelMapperAPI.MapBookingWrite(value);
                    newBooking.Id = id;
                    service.Update(id, newBooking);
                    logger.Info($"|BOOKING| Booking with id = {id} edited succesfully");
                    return Get(request, id);
                }
                logger.Info($"|BOOKING| Booking with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (NullReferenceException ex)
            {
                logger.Info($"|BOOKING| Booking with id = {id} not found");
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
