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
    class BookingRepository : IRepository<Booking>
    {
        private HotelContext db;

        public BookingRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public Booking Get(int id)
        {
            return db.Bookings.Find(id);
        }

        public void Create(Booking booking)
        {
            db.Bookings.Add(booking);
        }

        public void Delete(int id)
        {
            Booking booking = Get(id);
            if (booking != null)
                db.Bookings.Remove(booking);
        }

        public void Update(int id, Booking item)
        {
            Booking booking = Get(id);
            if (booking != null)
            {
                booking.BookingDate = item.BookingDate;
                booking.ArrivalDate = item.ArrivalDate;
                booking.CheckoutDate = item.CheckoutDate;
                booking.ClientId = item.ClientId;
                booking.RoomId = item.RoomId;
                booking.StatusId = item.StatusId;
            }
        }
    }
}
