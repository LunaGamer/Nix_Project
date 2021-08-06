using Nix_Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.EF
{
    public class HotelInitializer : DropCreateDatabaseAlways<HotelContext>
    {
        private void ClientInitializer(HotelContext context)
        {
            var clientList = new List<Client>()
            {
                new Client()
                {
                    Id = 1,
                    Name = "TestClientName1",
                    Surname = "TestClientSurname1"

                },
                new Client()
                {
                    Id = 2,
                    Name = "TestClientName2",
                    Surname = "TestClientSurname2"

                }
            };

            foreach (var client in clientList)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();
        }

        private void RoomInitializer(HotelContext context)
        {
            var roomList = new List<Room>()
            {
                new Room()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Room1",
                    Active = true
                },
                new Room()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Room2",
                    Active = true
                },
                new Room()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Room3-A",
                    Active = false
                }
            };

            foreach (var room in roomList)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();
        }

        private void CategoryInitializer(HotelContext context)
        {
            var categoryList = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Category1",
                    Beds = 1
                },
                new Category()
                {
                    Id = 2,
                    Name = "Category2",
                    Beds = 2
                }
            };

            foreach (var category in categoryList)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();
        }

        private void PriceInitializer(HotelContext context)
        {
            var priceList = new List<PriceCategory>()
            {
                new PriceCategory()
                {
                    Id = 1,
                    CategoryId = 1,
                    Price = 100,
                    StartDate = new DateTime(2021,01,01)
                },
                new PriceCategory()
                {
                    Id = 2,
                    CategoryId = 2,
                    Price = 300,
                    StartDate = new DateTime(2021,01,01)
                }
            };

            foreach (var price in priceList)
            {
                context.Prices.Add(price);
            }
            context.SaveChanges();
        }

        private void BookingInitializer(HotelContext context)
        {
            var bookingList = new List<Booking>()
            {
                new Booking()
                {
                    Id = 1,
                    ClientId = 1,
                    RoomId = 1,                    
                    BookingDate = new DateTime(2021, 7, 5),
                    ArrivalDate = new DateTime(2021, 7, 30),
                    CheckoutDate = new DateTime(2021, 8, 9),
                    StatusId = 2
                },
                new Booking()
                {
                    Id = 2,
                    ClientId = 2,
                    RoomId = 2,
                    BookingDate = new DateTime(2021, 5, 10),
                    ArrivalDate = new DateTime(2021, 5, 21),
                    CheckoutDate = new DateTime(2021, 5, 29),
                    StatusId = 3
                }
            };

            foreach (var booking in bookingList)
            {
                context.Bookings.Add(booking);
            }
            context.SaveChanges();
        }

        private void StatusInitializer(HotelContext context)
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

        protected override void Seed(HotelContext context)
        {
            ClientInitializer(context);
            CategoryInitializer(context);
            StatusInitializer(context);
            RoomInitializer(context);            
            PriceInitializer(context);
            BookingInitializer(context);
            
        }
    }
}
