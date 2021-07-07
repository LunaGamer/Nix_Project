using AutoMapper;
using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Interfaces;
using Nix_Hotel.DAL.Enteties;
using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Services
{
    public class RoomService : IRoomService
    {
        private IWorkUnit Database
        {
            get;
            set;
        }

        private IMapper mapperRead;
        private IMapper mapperWrite;
        private IMapper mapperCategory;

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<RoomDTO, Room>()
            .ForMember(room => room.CategoryId, opt => opt.MapFrom(c => c.Category.Id))).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            var rooms = Database.Rooms.GetAll();
            List<RoomDTO> roomsDTO = new List<RoomDTO>();
            foreach (var room in rooms)
            {
                var roomDTO = mapperRead.Map<Room, RoomDTO>(room);
                roomDTO.Category = mapperCategory.Map<Category, CategoryDTO>(room.RoomCategory);
                roomsDTO.Add(roomDTO);
            }
            return roomsDTO;
        }

        public RoomDTO Get(int id)
        {
            var room = Database.Rooms.Get(id);
            if (room != null)
            {
                var roomCategory = Database.Categories.Get(room.CategoryId);
                var roomDTO = mapperRead.Map<Room, RoomDTO>(room);
                roomDTO.Category = mapperCategory.Map<Category, CategoryDTO>(room.RoomCategory);
                return roomDTO;
            }
            return null;
        }

        public IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate || startDate.Date < DateTime.UtcNow.Date)
            {
                return null;
            }
            List<RoomDTO> freeRooms = GetAllRooms().Where(r => r.Active).ToList();
            foreach (var booking in Database.Bookings.GetAll())
            {
                if (booking.StatusId < 3 && !(booking.ArrivalDate > endDate || booking.CheckoutDate < startDate))
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

        public void Update(int id, RoomDTO room)
        {
            if (room != null)
            {
                var newRoom = mapperWrite.Map<RoomDTO, Room>(room);
                Database.Rooms.Update(id, newRoom);
                Database.Save();
            }
        }
    }
}
