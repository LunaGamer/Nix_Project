using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nix_Hotel.WEB.Models
{
    public class CategoryModel
    {
        [Required]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Category Name")]
        public string Name
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Number of beds")]
        public int Beds
        {
            get;
            set;
        }

        [Required]
        [DisplayName("Price for 1 night")]
        public decimal Price
        {
            get;
            set;
        }
    }
}