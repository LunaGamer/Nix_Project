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

        public decimal TotalCost
        {
            get;
            set;
        }
        public override bool Equals(object obj)
        {
            if (obj is BookingModel)
            {
                var booking = obj as BookingModel;
                return this.Id == booking.Id &&
                    this.BookingDate == booking.BookingDate &&
                    this.ArrivalDate == booking.ArrivalDate &&
                    this.CheckoutDate == booking.CheckoutDate &&
                    this.Client.Id == booking.Client.Id &&
                    this.Room.Id == booking.Room.Id &&
                    this.Status.Id == booking.Status.Id;
            }
            return base.Equals(obj);
        }
    }
}