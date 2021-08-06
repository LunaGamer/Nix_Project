using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Interfaces
{
    public interface IWorkUnit
    {
        IRepository<Client> Clients { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Category> Categories { get; }
        IRepository<PriceCategory> Prices { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Status> Statuses { get; }
        void Save();
    }
}
