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

        private IMapper mapper;

        public RoomService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()).CreateMapper();
        }

        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll());
        }

        public RoomDTO Get(int id)
        {
            return mapper.Map<Room, RoomDTO>(Database.Rooms.Get(id));
        }

        public IEnumerable<RoomDTO> GetFreeRooms(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return null;
            }
            List<RoomDTO> freeRooms = new List<RoomDTO>();
            freeRooms.AddRange(mapper.Map<IEnumerable<Room>, List<RoomDTO>>(Database.Rooms.GetAll()));
            foreach (var booking in Database.Bookings.GetAll())
            {
                if (booking.StatusId < 3 && !(booking.ArrivalDate > endDate || booking.CheckoutDate < startDate))
                {
                    freeRooms.Remove(mapper.Map<Room, RoomDTO>(booking.RoomBooking));
                }
            }
            return freeRooms;
        }
    }
}
