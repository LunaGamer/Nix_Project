using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.EF
{
    public class HotelInitializer : DropCreateDatabaseAlways<HotelModel>
    {
        private void ClientInitializer(HotelModel context)
        {
            var clientList = new List<Client>();

            foreach (var client in clientList)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();
        }

        private void RoomInitializer(HotelModel context)
        {
            var roomList = new List<Room>();

            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();
        }

        private void CategoryInitializer(HotelModel context)
        {
            var categoryList = new List<Category>();

            foreach (var category in categoryList)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }

        private void PriceInitializer(HotelModel context)
        {
            var priceList = new List<PriceCategory>();

            foreach (var price in priceList)
            {
                context.Prices.Add(price);
            }
            context.SaveChanges();
        }

        private void BookingInitializer(HotelModel context)
        {
            var bookingList = new List<Booking>();

            foreach (var booking in bookingList)
            {
                context.Bookings.Add(booking);
            }
            context.SaveChanges();
        }

        private void StatusInitializer(HotelModel context)
        {
            var statusList = new List<Status>()
            {
                new Status()
                {
                    Id = 1,
                    Name = "Booking"
                },

                new Status()
                {
                    Id = 2,
                    Name = "Occupied"
                },

                new Status()
                {
                    Id = 3,
                    Name = "Checked-out"
                },

                new Status()
                {
                    Id = 4,
                    Name = "Cancelled"
                },
            };

            foreach (var status in statusList)
            {
                context.Statuses.Add(status);
            }
            context.SaveChanges();
        }

        protected override void Seed(HotelModel context)
        {
            ClientInitializer(context);
            RoomInitializer(context);
            CategoryInitializer(context);
            PriceInitializer(context);
            BookingInitializer(context);
            StatusInitializer(context);
        }
    }
}
