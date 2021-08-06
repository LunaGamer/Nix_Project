using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class PriceCategoryModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public CategoryModel Category
        {
            get;
            set;
        }

        [Required]
        public decimal Price
        {
            get;
            set;
        }

        [Required]
        public DateTime StartDate
        {
            get;
            set;
        }

        public DateTime? EndDate
        {
            get;
            set;
        }
    }
}