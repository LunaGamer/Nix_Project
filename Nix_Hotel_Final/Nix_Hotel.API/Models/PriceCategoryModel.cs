﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Models
{
    public class PriceCategoryModel
    {
        public int Id
        {
            get;
            set;
        }

        public CategoryModel Category
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

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