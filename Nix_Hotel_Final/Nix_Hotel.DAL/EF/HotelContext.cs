using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.EF
{
    public class HotelContext : DbContext
    {
        public HotelContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<HotelContext>(new HotelInitializer());
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PriceCategory> Prices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
