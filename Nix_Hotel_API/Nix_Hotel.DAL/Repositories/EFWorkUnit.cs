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
    public class EFWorkUnit : IWorkUnit
    {
        private HotelModel db;
        private ClientRepository clientRepository;
        private RoomRepository roomRepository;
        private CategoryRepository categoryRepository;
        private PriceCategoryRepository priceRepository;
        private BookingRepository bookingRepository;
        private StatusRepository statusRepository;

        public EFWorkUnit(string connectionString)
        {
            db = new HotelModel(connectionString);
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                {
                    clientRepository = new ClientRepository(db);
                }

                return clientRepository;
            }
        }

        public IRepository<Room> Rooms
        {
            get
            {
                if (roomRepository == null)
                {
                    roomRepository = new RoomRepository(db);
                }

                return roomRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(db);
                }

                return categoryRepository;
            }
        }

        public IRepository<PriceCategory> Prices
        {
            get
            {
                if (priceRepository == null)
                {
                    priceRepository = new PriceCategoryRepository(db);
                }

                return priceRepository;
            }
        }

        public IRepository<Booking> Bookings
        {
            get
            {
                if (bookingRepository == null)
                {
                    bookingRepository = new BookingRepository(db);
                }

                return bookingRepository;
            }
        }

        public IRepository<Status> Statuses
        {
            get
            {
                if (statusRepository == null)
                {
                    statusRepository = new StatusRepository(db);
                }

                return statusRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
