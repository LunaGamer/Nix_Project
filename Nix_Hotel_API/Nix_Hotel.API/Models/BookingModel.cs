using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Models
{
    public class BookingModel
    {
        public int Id
        {
            get;
            set;
        }

        public ClientModel Client
        {
            get;
            set;
        }

        public RoomModel Room
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

        public StatusModel Status
        {
            get;
            set;
        }
    }
}