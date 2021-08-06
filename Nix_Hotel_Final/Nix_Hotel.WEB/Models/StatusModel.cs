using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class StatusModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Booking Status")]
        public string Name
        {
            get;
            set;
        }
    }
}