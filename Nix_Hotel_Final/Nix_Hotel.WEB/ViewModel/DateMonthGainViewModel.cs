using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.ViewModel
{
    public class DateMonthGainViewModel
    {
        [Required]
        [DisplayName("Month")]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [DisplayName("Year")]
        [Range(2000, 2100)]
        public int Year { get; set; }

        [DisplayName("Total Gane")]
        public decimal Gane { get; set; }
    }
}