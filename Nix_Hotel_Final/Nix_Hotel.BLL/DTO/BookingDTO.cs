using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.DTO
{
    public class BookingDTO
    {
        public int Id
        {
            get;
            set;
        }

        public ClientDTO Client
        {
            get;
            set;
        }

        public RoomDTO Room
        {
            get;
            set;
        }

        public DateTime? BookingDate
        {
            get;
            set;
        }

        public DateTime ArrivalDate
        {
            get;
            set;
        }

        public DateTime CheckoutDate
        {
            get;
            set;
        }

        public StatusDTO Status
        {
            get;
            set;
        }

        public decimal TotalCost
        {
            get;
            set;
        }
    }
}
