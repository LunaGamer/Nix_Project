using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.DAL.Enteties
{
    public class Booking
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        public int ClientId
        {
            get;
            set;
        }

        public int RoomId
        {
            get;
            set;
        }

        public DateTime? BookingDate
        {
            get;
            set;
        }

        public DateTime? ArrivalDate
        {
            get;
            set;
        }

        public DateTime? CheckoutDate
        {
            get;
            set;
        }

        public int StatusId
        {
            get;
            set;
        }

        [ForeignKey("ClientId")]
        public virtual Client ClientBooking
        {
            get;
            set;
        }

        [ForeignKey("RoomId")]
        public virtual Room RoomBooking
        {
            get;
            set;
        }

        [ForeignKey("StatusId")]
        public virtual Status StatusBooking
        {
            get;
            set;
        }
    }
}
