using Nix_Hotel.DAL.EF;
using Nix_Hotel.DAL.Enteties;
using Nix_Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Repositories
{
    class RoomRepository : IRepository<Room>
    {
        private HotelModel db;

        public RoomRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return db.Rooms;
        }

        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            db.Rooms.Add(room);
        }

        public void Delete(int id)
        {
            Room room = Get(id);
            if (room != null)
                db.Rooms.Remove(room);
        }
    }
}
