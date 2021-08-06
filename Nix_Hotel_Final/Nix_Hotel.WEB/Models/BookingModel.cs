using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class BookingModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public ClientModel Client
        {
            get;
            set;
        }

        [Required]
        public RoomModel Room
        {
            get;
            set;
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Booking Date")]
        public DateTime? BookingDate
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Arrival Date")]
        public DateTime ArrivalDate
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Check-out Date")]
        public DateTime CheckoutDate
        {
            get;
            set;
        }

        [Required]
        public StatusModel Status
        {
            get;
            set;
        }

        [DisplayName("Total Cost")]
        public decimal TotalCost
        {
            get;
            set;
        }

        [Range(int.MinValue, 0, ErrorMessage = "Arrival Date should be set before Check-out date")]
        public int DatesCorrect
        {
            get
            {
                return DateTime.Compare(ArrivalDate, CheckoutDate);
            }
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