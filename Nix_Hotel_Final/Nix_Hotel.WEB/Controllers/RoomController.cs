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
    public class RoomController : Controller
    {
        private IRoomService service;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RoomController(IRoomService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult ViewAll()
        {
            logger.Info("|ROOM| Selecting the list of all rooms");
            var rooms = HotelMapperWEB.MapRoomReadList(service.GetAllRooms());
            if (rooms != null)
            {
                logger.Info("|ROOM| List of all rooms was succesfully selected from DB");
                return View(rooms);
            }
            logger.Warn("|CLIENT| List of rooms was Empty");
            return RedirectToAction("Error", "Room");
        }

        [Authorize]
        public ActionResult View(int id)
        {
            logger.Info($"|ROOM| Selecting room with id = {id}");
            var room = HotelMapperWEB.MapRoomReadSingle(service.Get(id));
            if (room != null)
            {
                logger.Info($"|ROOM| Room with id = {id} was succesfully selected from DB");
                return View(room);
            }
            logger.Info($"|ROOM| Room with id = {id} not found");
            return RedirectToAction("Error", "Room");
        }


        [Authorize]
        public ActionResult Add()
        {
            SelectList categories = new SelectList(HotelMapperWEB.mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(service.GetCategories()).ToList(), "Id", "Name");
            ViewBag.categoryList = categories;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(RoomModel room)
        {
            logger.Info("|ROOM| Trying to create new room");
            SelectList categories = new SelectList(HotelMapperWEB.mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(service.GetCategories()).ToList(), "Id", "Name");
            ViewBag.categoryList = categories;
            ModelState.Remove("Category.Name");
            if (ModelState.IsValid)
            {
                logger.Info("|ROOM| New room was succesfully createed");
                int id = service.GetAllRooms().Count() + 1;
                room.Id = id;
                var newRoom = HotelMapperWEB.MapRoomWrite(room);
                service.Create(newRoom);
                return RedirectToAction("View", "Room", new { id = id });
            }
            logger.Info($"|ROOM| Can't create room, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(room);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            logger.Info($"|ROOM| Selecting room with id = {id} for editing");
            var oldRoom = HotelMapperWEB.MapRoomReadSingle(service.Get(id));
            if (oldRoom != null)
            {
                SelectList categories = new SelectList(HotelMapperWEB.mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(service.GetCategories()).ToList(), "Id", "Name", oldRoom.Category.Id);
                ViewBag.categoryList = categories;
                return View(oldRoom);
            }
            logger.Info($"|ROOM| Room with id = {id} not found");
            return RedirectToAction("Error", "Room");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RoomModel room)
        {
            logger.Info($"|ROOM| Editing room with id = {id}");
            SelectList categories = new SelectList(HotelMapperWEB.mapperCategory.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(service.GetCategories()).ToList(), "Id", "Name", room.Category.Id);
            ViewBag.categoryList = categories;
            ModelState.Remove("Category.Name");
            if (ModelState.IsValid)
            {
                logger.Info($"|ROOM| Room with id = {id} edited succesfully");
                var newRoom = HotelMapperWEB.MapRoomWrite(room);
                service.Update(room.Id, newRoom);
                return RedirectToAction("View", "Room", new { id = room.Id });
            }
            logger.Info($"|ROOM| Can't edit room, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(room);
        }

        public ActionResult FreeRooms()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FreeRooms(DatesRoomViewModel model)
        {
            logger.Info("|ROOM| Trying to find free rooms");
            if (ModelState.IsValid)
            {
                var rooms = service.GetFreeRooms(model.Start, model.End);
                if (rooms != null && rooms.Any())
                {
                    logger.Info("|ROOM| Free rooms found");
                    model.Rooms = HotelMapperWEB.MapRoomReadList(rooms).ToList();
                    return View(model);
                }
                logger.Info("|ROOM| Couldn't find free rooms");
                return View(model);
            }
            logger.Info($"|ROOM| Incorrect dates, {ModelState.Values.FirstOrDefault(x => x.Errors.Count > 0).Errors.FirstOrDefault().ErrorMessage}");
            return View(model);
        }
    }
}