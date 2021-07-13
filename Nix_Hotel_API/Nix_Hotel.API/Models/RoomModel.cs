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

        public override bool Equals(object obj)
        {
            if (obj is RoomModel)
            {
                var room = obj as RoomModel;
                return this.Id == room.Id &&
                    this.Name == room.Name &&
                    this.Category.Id == room.Category.Id &&
                    this.Active == room.Active;
            }
            return base.Equals(obj);
        }
    }
}