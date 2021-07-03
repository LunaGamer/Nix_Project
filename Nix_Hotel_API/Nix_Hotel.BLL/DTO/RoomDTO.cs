﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nix_Hotel.BLL.DTO
{
    public class RoomDTO
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

        public CategoryDTO Category
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }
    }
}
