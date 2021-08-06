using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.BLL.Infrastructure;
using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nix_Hotel.DAL.Enteties;

namespace Nix_Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IWorkUnit Database
        {
            get;
            set;
        }

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            var rooms = HotelMapperBLL.MapRoomReadList(Database.Rooms.GetAll());
            foreach (var room in rooms)
            {
                room.Category = GetCategory(room.Category.Id);
            }
            return rooms;
        }

        public RoomDTO Get(int id)
        {
            var room = HotelMapperBLL.MapRoomReadSingle(Database.Rooms.Get(id));
            room.Category = GetCategory(room.Category.Id);
            return room;
        }

        public IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return null;
            }
            List<RoomDTO> freeRooms = GetAllRooms().Where(r => r.Active).ToList();
            foreach (var booking in Database.Bookings.GetAll())
            {
                if (booking.StatusId < 4 && !(booking.ArrivalDate > endDate || booking.CheckoutDate < startDate))
                {
                    var roomDelete = freeRooms.Find(x => x.Id == booking.RoomId);
                    if (roomDelete != null)
                    {
                        freeRooms.Remove(roomDelete);
                    }
                }
            }
            return freeRooms;
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            var categories = HotelMapperBLL.mapperCategory.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(Database.Categories.GetAll());
            foreach (var category in categories)
            {
                category.Price = GetCategoryPrice(category.Id);
            }
            return categories;
        }

        public CategoryDTO GetCategory(int id)
        {
            var category = HotelMapperBLL.mapperCategory.Map<Category, CategoryDTO>(Database.Categories.Get(id));
            category.Price = GetCategoryPrice(id);
            return category;
        }

        public decimal GetCategoryPrice(int id)
        {
            var price = Database.Prices.GetAll().ToList().Find(x => x.CategoryId == id && DateTime.Now >= x.StartDate && (x.EndDate == null || DateTime.Now <= x.EndDate)).Price;
            return price;
        }

        public void Create(RoomDTO roomDTO)
        {
            if (roomDTO != null)
            {
                var room = HotelMapperBLL.MapRoomWrite(roomDTO);
                Database.Rooms.Create(room);
                Database.Save();
            }
        }

        public void Update(int id, RoomDTO roomDTO)
        {
            if (roomDTO != null)
            {
                var room = HotelMapperBLL.MapRoomWrite(roomDTO);
                Database.Rooms.Update(id, room);
                Database.Save();
            }
        }
    }
}
