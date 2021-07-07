using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nix_Hotel.API.Models
{
    public class RoomModel
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

        /*public int CategoryId
        {
            get;
            set;
        }*/

        public CategoryModel Category
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