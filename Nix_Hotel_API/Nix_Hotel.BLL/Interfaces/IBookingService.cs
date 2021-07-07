using Nix_Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDTO> GetAllBookings();
        BookingDTO Get(int id);
        void Create(BookingDTO booking);
        void Update(int id, BookingDTO booking);
        decimal Gain(DateTime month);
    }
}
