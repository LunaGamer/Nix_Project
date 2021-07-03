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
    class StatusRepository : IRepository<Status>
    {
        private HotelModel db;

        public StatusRepository(HotelModel db)
        {
            this.db = db;
        }

        public IEnumerable<Status> GetAll()
        {
            return db.Statuses;
        }

        public Status Get(int id)
        {
            return db.Statuses.Find(id);
        }

        public void Create(Status status)
        {
            db.Statuses.Add(status);
        }

        public void Delete(int id)
        {
            Status status = Get(id);
            if (status != null)
                db.Statuses.Remove(status);
        }
    }
}
