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
    public class BookingService : IBookingService
    {
        private IWorkUnit Database
        {
            get;
            set;
        }

        private IMapper mapper;

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()).CreateMapper();
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            return mapper.Map<IEnumerable<Booking>, List<BookingDTO>>(Database.Bookings.GetAll());
        }

        public BookingDTO Get(int id)
        {
            return mapper.Map<Booking, BookingDTO>(Database.Bookings.Get(id));
        }

        public void Create(BookingDTO booking)
        {
            var mapperCreate = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, Booking>()).CreateMapper();
            Database.Bookings.Create(mapperCreate.Map<BookingDTO, Booking>(booking));
        }

        public decimal Gain(DateTime month)
        {
            return 0;
        }
    }
}
