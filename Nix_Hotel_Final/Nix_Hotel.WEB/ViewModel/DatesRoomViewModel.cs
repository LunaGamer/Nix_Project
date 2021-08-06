using Nix_Hotel.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.ViewModel
{
    public class DatesRoomViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Starting Date")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Ending Date")]
        public DateTime End { get; set; }

        [Range(int.MinValue, 0, ErrorMessage = "Starting Date should be set before Ending date")]
        public int DatesCorrect
        {
            get
            {
                return DateTime.Compare(Start, End);
            }
        }

        public List<RoomModel> Rooms
        { 
            get;
            set;
        }
    }
}