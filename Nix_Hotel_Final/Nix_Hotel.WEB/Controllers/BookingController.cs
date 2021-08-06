using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.WEB.Models;
using Nix_Hotel.WEB.Utils;
using Nix_Hotel.WEB.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nix_Hotel.WEB.Controllers
{
    public class BookingController : Controller
    {
        private IBookingService service;
        private IClientService clientService;
        private IRoomService roomService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BookingController(IBookingService service, IClientService clientService, IRoomService roomService)
        {
            this.service = service;
            this.clientService = clientService;
            this.roomService = roomService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewAll()
        {
            logger.Info("|BOOKING| Selecting the list of all bookings");
            var bookings = HotelMapperWEB.MapBookingReadList(service.GetAllBookings());
            if (bookings != null)
            {
                logger.Info("|BOOKING| List of all bookings was succesfully selected from DB");
                return View(bookings);
            }
            logger.Warn("|BOOKING| List of bookings was Empty");
            return RedirectToAction("Error", "Booking");
        }

        [Authorize]
        public ActionResult View(int id)
        {
            logger.Info($"|BOOKING| Selecting booking with id = {id}");
            var booking = HotelMapperWEB.MapBookingReadSingle(service.Get(id));
            if (booking != null)
            {
                logger.Info($"|BOOKING| Booking with id = {id} was succesfully selected from DB");
                return View(booking);
            }
            logger.Info($"|BOOKING| Booking with id = {id} not found");
            return RedirectToAction("Error", "Booking");
        }

        [Authorize]
        public ActionResult Add()
        {
            SelectList clients = new SelectList(HotelMapperWEB.mapperClient.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(clientService.GetAllClients()).ToList(), "Id", "ClientFullName");
            SelectList rooms = new SelectList(HotelMapperWEB.MapRoomReadList(roomService.GetAllRooms()).ToList(), "Id", "Name");
            SelectList statuses = new SelectList(HotelMapperWEB.mapperStatus.Map<IEnumerable<StatusDTO>, IEnumerable<StatusModel>>(service.GetStatuses()).Where(x => x.Id < 3).ToList(), "Id", "Name");
            ViewBag.clientList = clients;
            ViewBag.roomList = rooms;
            ViewBag.statusList = statuses;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BookingModel booking)
        {
            logger.Info("|BOOKING| Trying to create new booking");
            SelectList clients = new SelectList(HotelMapperWEB.mapperClient.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(clientService.GetAllClients()).ToList(), "Id", "ClientFullName");
            SelectList rooms = new SelectList(HotelMapperWEB.MapRoomReadList(roomService.GetAllRooms()).ToList(), "Id", "Name");
            SelectList statuses = new SelectList(HotelMapperWEB.mapperStatus.Map<IEnumerable<StatusDTO>, IEnumerable<StatusModel>>(service.GetStatuses()).Where(x => x.Id < 3).ToList(), "Id", "Name");
            ViewBag.clientList = clients;
            ViewBag.roomList = rooms;
            ViewBag.statusList = statuses;
            ModelState.Remove("Client.Name");
            ModelState.Remove("Client.Surname");
            ModelState.Remove("Room.Name");
            ModelState.Remove("Room.Category");
            ModelState.Remove("Status.Name");
            if (ModelState.IsValid)
            {
                var RoomIsFree = roomService.GetFreeRooms(booking.ArrivalDate, booking.CheckoutDate).ToList().Find(x => x.Id == booking.Room.Id) != null;
                if (RoomIsFree)
                {
                    logger.Info("|BOOKING| New booking was succesfully createed");
                    int id = service.GetAllBookings().Count() + 1;
                    var newBooking = HotelMapperWEB.MapBookingWrite(booking);
                    service.Create(newBooking);
                    return RedirectToAction("View", "Booking", new { id = id });
                }
                else
                {
                    ModelState.AddModelError("CheckoutDate", "Couldn't find free rooms for these dates");
                    return View(booking);
                }
            }
            logger.Info($"|BOOKING| Can't create booking, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(booking);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            logger.Info($"|BOOKING| Selecting booking with id = {id} for editing");
            var oldBooking = HotelMapperWEB.MapBookingReadSingle(service.Get(id));
            if (oldBooking != null)
            {
                logger.Info($"|BOOKING| Starting editing booking with id = {id}");
                SelectList clients = new SelectList(HotelMapperWEB.mapperClient.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(clientService.GetAllClients()).ToList(), "Id", "ClientFullName", oldBooking.Client.Id);
                SelectList rooms = new SelectList(HotelMapperWEB.MapRoomReadList(roomService.GetAllRooms()).ToList(), "Id", "Name", oldBooking.Room.Id);
                SelectList statuses = new SelectList(HotelMapperWEB.mapperStatus.Map<IEnumerable<StatusDTO>, IEnumerable<StatusModel>>(service.GetStatuses()).ToList(), "Id", "Name", oldBooking.Status.Id);
                ViewBag.clientList = clients;
                ViewBag.roomList = rooms;
                ViewBag.statusList = statuses;
                return View(oldBooking);
            }
            logger.Info($"|BOOKING| Booking with id = {id} not found");
            return RedirectToAction("Error", "Booking");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookingModel booking, FormCollection form)
        {
            logger.Info($"|BOOKING| Editing booking with id = {id}");
            SelectList clients = new SelectList(HotelMapperWEB.mapperClient.Map<IEnumerable<ClientDTO>, IEnumerable<ClientModel>>(clientService.GetAllClients()).ToList(), "Id", "ClientFullName", booking.Client.Id);
            SelectList rooms = new SelectList(HotelMapperWEB.MapRoomReadList(roomService.GetAllRooms()).ToList(), "Id", "Name", booking.Room.Id);
            SelectList statuses = new SelectList(HotelMapperWEB.mapperStatus.Map<IEnumerable<StatusDTO>, IEnumerable<StatusModel>>(service.GetStatuses()).ToList(), "Id", "Name", booking.Status.Id);
            ViewBag.clientList = clients;
            ViewBag.roomList = rooms;
            ViewBag.statusList = statuses;
            ModelState.Remove("Client.Name");
            ModelState.Remove("Client.Surname");
            ModelState.Remove("Room.Name");
            ModelState.Remove("Room.Category");
            ModelState.Remove("Status.Name");
            if (ModelState.IsValid)
            {
                logger.Info($"|BOOKING| Booking with id = {id} edited succesfully");
                var newBooking = HotelMapperWEB.MapBookingWrite(booking);
                service.Update(booking.Id, newBooking);
                return RedirectToAction("View", "Booking", new { id = booking.Id });
            }
            logger.Info($"|BOOKING| Can't edit booking, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(booking);
        }

        [Authorize]
        public ActionResult Gain()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gain(DateMonthGainViewModel model)
        {
            logger.Info($"|BOOKING| Trying to calculate gain");
            if (ModelState.IsValid)
            {
                logger.Info($"|BOOKING| Gain for Year = {model.Year}, Month = {model.Month} succesfully calculated");
                DateTime month = new DateTime(model.Year, model.Month, 1);
                var gane = service.Gain(month);
                model.Gane = gane;
                return View(model);
            }
            logger.Info($"|BOOKING| Can't calculate gain, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(model);
        }
    }
}