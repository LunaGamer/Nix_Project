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

        private IMapper mapperRead;
        private IMapper mapperWrite;
        private IMapper mapperRoom;
        private IMapper mapperCategory;
        private IMapper mapperClient;
        private IMapper mapperStatus;

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
            mapperRead = new MapperConfiguration(cfg => cfg.CreateMap<Booking, BookingDTO>()
            .ForMember(b => b.Client, opt => opt.Ignore())
            .ForMember(b => b.Room, opt => opt.Ignore())
            .ForMember(b => b.Status, opt => opt.Ignore())).CreateMapper();
            mapperWrite = new MapperConfiguration(cfg => cfg.CreateMap<BookingDTO, Booking>()
            .ForMember(b => b.ClientId, opt => opt.MapFrom(c => c.Client.Id))
            .ForMember(b => b.RoomId, opt => opt.MapFrom(r => r.Room.Id))
            .ForMember(b => b.StatusId, opt => opt.MapFrom(s => s.Status.Id))).CreateMapper();
            mapperRoom = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomDTO>()
            .ForMember(room => room.Category, opt => opt.Ignore())).CreateMapper();
            mapperCategory = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            mapperClient = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientDTO>()).CreateMapper();
            mapperStatus = new MapperConfiguration(cfg => cfg.CreateMap<Status, StatusDTO>()).CreateMapper();
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            var bookings = Database.Bookings.GetAll();
            List<BookingDTO> bookingsDTO = new List<BookingDTO>();
            foreach (var booking in bookings)
            {
                var bookingDTO = mapperRead.Map<Booking, BookingDTO>(booking);
                bookingDTO.Room = mapperRoom.Map<Room, RoomDTO>(booking.BookingRoom);
                bookingDTO.Room.Category = mapperCategory.Map<Category, CategoryDTO>(booking.BookingRoom.RoomCategory);
                bookingDTO.Client = mapperClient.Map<Client, ClientDTO>(booking.BookingClient);
                bookingDTO.Status = mapperStatus.Map<Status, StatusDTO>(booking.BookingStatus);
                bookingsDTO.Add(bookingDTO);
            }
            return bookingsDTO;
        }

        public BookingDTO Get(int id)
        {
            var booking = Database.Bookings.Get(id);
            if (booking != null)
            {
                var bookingDTO = mapperRead.Map<Booking, BookingDTO>(booking);
                bookingDTO.Room = mapperRoom.Map<Room, RoomDTO>(booking.BookingRoom);
                bookingDTO.Room.Category = mapperCategory.Map<Category, CategoryDTO>(booking.BookingRoom.RoomCategory);
                bookingDTO.Client = mapperClient.Map<Client, ClientDTO>(booking.BookingClient);
                bookingDTO.Status = mapperStatus.Map<Status, StatusDTO>(booking.BookingStatus);
                return bookingDTO;
            }
            return null;
        }

        public void Create(BookingDTO booking)
        {
            if (booking != null)
            {
                Database.Bookings.Create(mapperWrite.Map<BookingDTO, Booking>(booking));
                Database.Save();
            }
        }

        public decimal Gain(DateTime date)
        {
            decimal gain = 0;
            var bookings = Database.Bookings.GetAll();
            foreach (var booking in bookings)
            {
                if (booking.ArrivalDate.Year == date.Year && booking.ArrivalDate.Month == date.Month)
                {
                    var days = (booking.CheckoutDate - booking.ArrivalDate).Days;
                    var price = Database.Prices.GetAll().ToList().Find(x => booking.BookingRoom.CategoryId == x.CategoryId
                    && booking.ArrivalDate >= x.StartDate && (x.EndDate == null || booking.CheckoutDate <= x.EndDate));
                    gain += days * price.Price;
                }
            }
            return gain;
        }

        public void Update(int id, BookingDTO booking)
        {
            if (booking != null)
            {
                var newBooking = mapperWrite.Map<BookingDTO, Booking>(booking);
                Database.Bookings.Update(id, newBooking);
                Database.Save();
            }
        }
    }
}
