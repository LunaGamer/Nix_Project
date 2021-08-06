using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Models
{
    public class CategoryModel
    {
        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Beds
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
    }
}