using Nix_Hotel.BLL.DTO;
using Nix_Hotel.BLL.Infrastructure;
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

        public BookingService(IWorkUnit database)
        {
            this.Database = database;
        }

        public IEnumerable<BookingDTO> GetAllBookings()
        {
            var bookingsDTO = HotelMapperBLL.MapBookingReadList(Database.Bookings.GetAll());
            foreach (var booking in bookingsDTO)
            {
                booking.TotalCost = GetBookingCost(booking.Id);
            }
            return bookingsDTO;
        }

        public IEnumerable<StatusDTO> GetStatuses()
        {
            var statuses = Database.Statuses.GetAll();
            return HotelMapperBLL.mapperStatus.Map<IEnumerable<Status>, IEnumerable<StatusDTO>>(statuses);
        }

        public BookingDTO Get(int id)
        {
            var bookingDTO = HotelMapperBLL.MapBookingReadSingle(Database.Bookings.Get(id));
            bookingDTO.TotalCost = GetBookingCost(bookingDTO.Id);
            return bookingDTO;
        }

        public decimal GetBookingCost(int id)
        {
            var booking = Database.Bookings.Get(id);
            var prices = Database.Prices.GetAll();
            var dateTemp = new DateTime(booking.ArrivalDate.Ticks);
            decimal cost = 0;
            do
            {
                cost += prices.ToList().Find(x => dateTemp >= x.StartDate && (x.EndDate == null || dateTemp <= x.EndDate)).Price;
                dateTemp = dateTemp.AddDays(1);
            } while (dateTemp <= booking.CheckoutDate);
            return cost;

        }

        public void Create(BookingDTO bookingDTO)
        {
            if (bookingDTO != null)
            {
                var booking = HotelMapperBLL.MapBookingWrite(bookingDTO);
                Database.Bookings.Create(booking);
                Database.Save();
            }
        }

        public decimal Gain(DateTime date)
        {
            decimal gain = 0;
            var bookings = Database.Bookings.GetAll().ToList().Where(x => x.StatusId < 4 && ((x.ArrivalDate.Year == date.Year && x.ArrivalDate.Month == date.Month) || (x.CheckoutDate.Year == date.Year && x.CheckoutDate.Month == date.Month)));
            var prices = Database.Prices.GetAll();
            var dateLastDayOfMonth = new DateTime(date.Year, date.Month, date.Day).AddMonths(1);
            foreach (var booking in bookings)
            {
                var dateTemp = new DateTime(date.Ticks);
                do
                {
                    if (dateTemp >= booking.ArrivalDate && dateTemp <= booking.CheckoutDate)
                    {
                        gain += prices.ToList().Find(x => dateTemp >= x.StartDate && (x.EndDate == null || dateTemp <= x.EndDate)).Price;
                    }
                    dateTemp = dateTemp.AddDays(1);
                } while (dateTemp < dateLastDayOfMonth);
            }
            return gain;
        }

        public void Update(int id, BookingDTO bookingDTO)
        {
            if (bookingDTO != null)
            {
                var booking = HotelMapperBLL.MapBookingWrite(bookingDTO);
                Database.Bookings.Update(id, booking);
                Database.Save();
            }
        }
    }
}
